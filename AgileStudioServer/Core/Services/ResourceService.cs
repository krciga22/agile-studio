using AgileStudioServer.Core.Exceptions;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;

namespace AgileStudioServer.Core.Services
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
    }
}
