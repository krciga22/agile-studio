using AgileStudioServer.Features.Auth.Scopes;

namespace AgileStudioServer.Core.Resources
{
    public interface IResourceMap
    {
        string GetResourceType();

        string GetResourcePermissionScope();

        Type GetResourceModelType();

        Type GetResourceDtoType();

        Type GetResourceDtoCreateType();

        Type GetResourceDtoUpdateType();

        Type GetResourceModelServiceType();

        Type? GetResourceModelRepositoryType();

        ParentScope GetParentResourceScope(Object model);

        bool IsPermissionedResource();
    }
}
