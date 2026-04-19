using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.Releases
{

    public class ReleasesResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.ReleasesRelease;
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

        public Type GetResourceServiceType()
        {
            return typeof(ReleaseService);
        }
    }
}