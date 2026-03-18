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
        public PaginationResults<object> GetAll(string type, ServiceContext serviceContext)
        {
            IResourceRepository? repository = _ResourceRepositories.FirstOrDefault(
                repo => repo.IsTypeSupported(type));

            if (repository == null) {
                throw new UnsupportedResourceTypeException(type);
            }

            return repository.GetAllResources(serviceContext);
        }

        public object? Get(string type, int id, ServiceContext serviceContext)
        {
            IResourceRepository? repository = _ResourceRepositories.FirstOrDefault(
                repo => repo.IsTypeSupported(type));

            if (repository == null){
                throw new UnsupportedResourceTypeException(type);
            }

            return repository.GetResource(id, serviceContext);
        }

        public object Create(string type, object data, ServiceContext serviceContext)
        {
            IResourceRepository? repository = _ResourceRepositories.FirstOrDefault(
                repo => repo.IsTypeSupported(type));

            if (repository == null){
                throw new UnsupportedResourceTypeException(type);
            }

            return repository.CreateResource(data, serviceContext);
        }
    }
}
