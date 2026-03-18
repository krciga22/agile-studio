using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Resources.Resource
{
    public interface IResourceRepository
    {
        bool IsTypeSupported(string type);

        public Type GetResourceDtoType();

        public Type GetCreateResourceDtoType();

        public Type GetResourceModelType();

        PaginationResults<object> GetAllResources(ServiceContext serviceContext);

        object? GetResource(int id);

        object CreateResource(object data);
    }
}
