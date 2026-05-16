using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
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
            return Scopes.PROJECT_RELEASE;
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

        public ParentScope GetParentResourceScope(Object model)
        {
            ReleaseModel releaseModel = ((ReleaseModel)model);
            return new ParentScope(Scopes.PROJECT, releaseModel.ProjectID.ToString());
        }
    }
}