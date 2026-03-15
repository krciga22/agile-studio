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
                throw new Exception($"Resource type '{type}' is not supported");
            }


            return repository.GetAllResources(serviceContext);
        }
    }
}
