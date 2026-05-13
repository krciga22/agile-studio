using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.Releases
{

    public class ReleasesResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.ReleasesRelease;
        }

        public string GetResourcePermissionScope()
        {
            throw new NotImplementedException();
        }

        public Type GetResourceModelType()
        {
            return typeof(ReleaseModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(ReleaseDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(ReleasePostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(ReleasePatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(ReleaseService);
        }
    }
}