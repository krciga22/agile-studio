using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Resources;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Resources.Resource.Commands;
using AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions;
using System.Text.Json;

namespace AgileStudioServer.CoreFeatures.Resources.Resource
{
    public class ResourceService(
        IEnumerable<Repository> resourceRepositories,
        IEnumerable<IResourceMap> resourceMaps,
        CommandDispatcher commandDispatcher
        ) : AbstractService
    {
        private readonly IEnumerable<Repository> _Repositories = resourceRepositories;
        private readonly IEnumerable<IResourceMap> _ResourceMaps = resourceMaps;
        private readonly CommandDispatcher _CommandDispatcher = commandDispatcher;

        /// <summary>
        /// Get a paginated list of resources/models of the specified type.
        /// </summary>
        public PaginationResults<object> GetCollection(string type, ServiceContext serviceContext)
        {
            Repository repository = GetResourceRepository(type);

            object? result = (repository.GetType().GetMethod("GetAll")?.Invoke(repository, [serviceContext])) ??
                throw new Exception($"Failed to get all for resource of type {type}.");

            var resultType = result.GetType();
            var itemsProp = resultType.GetProperty("Items");
            var totalProp = resultType.GetProperty("Total");
            var pageProp = resultType.GetProperty("Page");
            var itemsPerPageProp = resultType.GetProperty("ItemsPerPage");

            var items = ((IEnumerable<object>)itemsProp.GetValue(result)!).Cast<object>().ToList();
            int total = (int)totalProp.GetValue(result)!;
            int page = (int)pageProp.GetValue(result)!;
            int itemsPerPage = (int)itemsPerPageProp.GetValue(result)!;

            return new PaginationResults<object>(items, total, page, itemsPerPage);
        }

        /// <summary>
        /// Get a resource/model of the specified type and ID.
        /// </summary>
        /// <exception cref="ResourceNotFoundException"></exception>
        public object Get(string type, object[] id, ServiceContext serviceContext)
        {
            Repository repository = GetResourceRepository(type);

            var identifier = (repository.GetType().GetMethod("ToIdentifier")?.Invoke(repository, [id])) ??
                throw new Exception(type);

            object? result = (repository.GetType().GetMethod("Get")?.Invoke(repository, [identifier])) ??
                throw new ResourceNotFoundException(type, id);

            return result;
        }

        /// <summary>
        /// Creates a resource/model of the specified type with the provided model.
        /// </summary>
        public object Create(string type, object model, ServiceContext serviceContext)
        {
            Repository repository = GetResourceRepository(type);

            _CommandDispatcher.Dispatch(
                new ResourceCreatingCommand(type, model, serviceContext));

            object result = (repository.GetType().GetMethod("Create")?.Invoke(repository, [model])) ?? 
                throw new Exception($"Failed to create resource of type {type}.");

            _CommandDispatcher.Dispatch(
                new ResourceCreatedCommand(type, result, serviceContext));

            return result;
        }

        /// <summary>
        /// Updates a resources/model of the specified type with the provided model.
        /// </summary>
        public object Update(string type, object[] id, object model, ServiceContext serviceContext)
        {
            Repository repository = GetResourceRepository(type);

            AssertExists(type, id);

            var identifierFromModel = (repository.GetType().GetMethod("GetIdentifier")?.Invoke(repository, [model])) ??
                throw new Exception($"Failed to get identifier for resource of type {type}.");

            var givenIdentifier = (repository.GetType().GetMethod("ToIdentifier")?.Invoke(repository, [id])) ??
                throw new Exception($"Failed to convert identifier for resource of type {type}.");

            var identifierFromModelJson = JsonSerializer.Serialize(identifierFromModel);
            var givenIdentifierJson = JsonSerializer.Serialize(givenIdentifier);
            if (identifierFromModelJson != givenIdentifierJson){
                throw new ResourceIdentifierMismatchException(id);
            }

            _CommandDispatcher.Dispatch(
                new ResourceUpdatingCommand(type, id, model, serviceContext));

            object result = (repository.GetType().GetMethod("Update")?.Invoke(repository, [model])) ??
                throw new Exception($"Failed to update resource of type {type}.");

            _CommandDispatcher.Dispatch(
                new ResourceUpdatedCommand(type, id, model, serviceContext));

            return result;
        }

        /// <summary>
        /// Deletes a resources/model of the specified type with the provided id.
        /// </summary>
        public void Delete(string type, object[] id, ServiceContext serviceContext)
        {
            Repository repository = GetResourceRepository(type);

            AssertExists(type, id);

            _CommandDispatcher.Dispatch(
                new ResourceDeletingCommand(type, id, serviceContext));

            var model = Get(type, id, serviceContext);

            _CommandDispatcher.Dispatch(
                new ResourceDeletedCommand(type, id, serviceContext));

            repository.GetType().GetMethod("Delete")?.Invoke(repository, [model]);
        }

        /// <summary>
        /// Assert a resource/model of the specified type and ID exists.
        /// </summary>
        /// <exception cref="ResourceNotFoundException">
        /// Thrown when the resource/model of the specified type and ID is not found.
        /// </exception>
        public void AssertExists(string type, object[] id)
        {
            Repository repository = GetResourceRepository(type);

            var identifier = (repository.GetType().GetMethod("ToIdentifier")?.Invoke(repository, [id])) ??
                throw new Exception($"Failed to convert identifier for resource of type {type}.");

            object? result = repository.GetType().GetMethod("Exists")?.Invoke(repository, [identifier]);
            if(result == null){
                throw new Exception($"Failed to check existence of resource of type {type}.");
            }

            bool exists = (bool)result;
            if (!exists){
                throw new ResourceNotFoundException(type, id);
            }
        }

        /// <exception cref="UnsupportedResourceTypeException">
        /// Thrown when no repository supports the given type.
        /// </exception>
        public Repository GetResourceRepository(string type)
        {
            var resourceMap = GetResourceMap(type);

            Repository? repository = _Repositories.FirstOrDefault(
                repo => repo.GetType() == resourceMap.GetResourceRepositoryType());

            if(repository == null){
                throw new UnsupportedResourceTypeException(type);
            }

            return repository;
        }

        /// <exception cref="UnsupportedResourceTypeException">
        /// Thrown when no resource map exists for the given type.
        /// </exception>
        private IResourceMap GetResourceMap(string type)
        {
            var resourceMap = _ResourceMaps.FirstOrDefault(r =>
                    r.GetResourceType() == type);
            if (resourceMap == null)
            {
                throw new UnsupportedResourceTypeException(type);
            }

            return resourceMap;
        }
    }
}
