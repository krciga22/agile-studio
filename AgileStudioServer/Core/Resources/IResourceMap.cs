namespace AgileStudioServer.Core.Resources
{
    public interface IResourceMap
    {
        string GetResourceType();

        Type GetResourceModelType();

        Type GetResourceDtoType();

        Type GetResourceDtoCreateType();

        Type GetResourceDtoUpdateType();

        Type GetResourceServiceType();
    }
}
