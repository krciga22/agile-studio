namespace AgileStudioServer.Core.Resources
{
    public interface IResourceMap
    {
        string GetResourceType();

        string GetResourcePermissionScope();

        string GetResourceReadPermissionKey();

        Type GetResourceModelType();

        Type GetResourceDtoType();

        Type GetResourceDtoCreateType();

        Type GetResourceDtoUpdateType();

        Type GetResourceModelServiceType();
    }
}
