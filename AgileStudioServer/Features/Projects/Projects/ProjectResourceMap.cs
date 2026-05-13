using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.ProjectsProject;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.PROJECT;
        }

        public Type GetResourceModelType()
        {
            return typeof(ProjectModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(ProjectDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(ProjectPostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(ProjectPatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(ProjectService);
        }
    }
}
