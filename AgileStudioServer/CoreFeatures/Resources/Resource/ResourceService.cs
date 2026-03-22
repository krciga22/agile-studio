using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions;

namespace AgileStudioServer.CoreFeatures.Resources.Resource
{
    public class ResourceService : AbstractService
    {
        private IEnumerable<IResourceRepository> _ResourceRepositories;

        public ResourceService(IEnumerable<IResourceRepository> resourceRepositories)
        {
            _ResourceRepositories = resourceRepositories;
        }

        /// <summary>
        /// Get a paginated list of resources/models of the specified type.
        /// </summary>
        public PaginationResults<object> GetAll(string type, ServiceContext serviceContext)
        {
            IResourceRepository repository = GetResourceRepository(type);
            return repository.GetAllResources(serviceContext);
        }

        /// <summary>
        /// Get a resources/model of the specified type and ID.
        /// </summary>
        /// <exception cref="ResourceNotFoundException">
        /// Thrown when the resource of the specified type and ID is not found.
        /// </exception>
        public object Get(string type, int id)
        {
            IResourceRepository repository = GetResourceRepository(type);
            var model = repository.GetResource(id);
            if(model == null){
                throw new ResourceNotFoundException(type, id);
            }

            return model;
        }

        /// <summary>
        /// Assert a resource/model of the specified type and ID exists.
        /// </summary>
        /// <exception cref="ResourceNotFoundException">
        /// Thrown when the resource/model of the specified type and ID is not found.
        /// </exception>
        public void AssertExists(string type, int id)
        {
            IResourceRepository repository = GetResourceRepository(type);
            bool exists = repository.IsResource(id);
            if(!exists){
                throw new ResourceNotFoundException(type, id);
            }
        }

        /// <summary>
        /// Creates a resources/model of the specified type with the provided model.
        /// </summary>
        public object Create(string type, object model)
        {
            IResourceRepository repository = GetResourceRepository(type);
            return repository.CreateResource(model);
        }

        /// <summary>
        /// Updates a resources/model of the specified type with the provided model.
        /// </summary>
        public object Update(string type, int id, object model)
        {
            IResourceRepository repository = GetResourceRepository(type);
            return repository.UpdateResource(id, model);
        }

        /// <exception cref="UnsupportedResourceTypeException">
        /// Thrown when no repository supports the given type.
        /// </exception>
        public IResourceRepository GetResourceRepository(string type)
        {
            IResourceRepository? repository = _ResourceRepositories.FirstOrDefault(
                repo => repo.IsTypeSupported(type));

            if(repository == null){
                throw new UnsupportedResourceTypeException(type);
            }

            return repository;
        }
    }
}
