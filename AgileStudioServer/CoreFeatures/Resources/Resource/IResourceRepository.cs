using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Resources.Resource
{
    public interface IResourceRepository
    {
        bool IsTypeSupported(string type);

        PaginationResults<object> GetAllResources(ServiceContext serviceContext);

        object? GetResource(int id, ServiceContext serviceContext);

        object CreateResource(object data, ServiceContext serviceContext);
    }
}
