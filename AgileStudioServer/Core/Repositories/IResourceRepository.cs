using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Core.Repositories
{
    public interface IResourceRepository
    {
        bool IsTypeSupported(string type);

        PaginationResults<object> GetAllResources(ServiceContext serviceContext);
    }
}
