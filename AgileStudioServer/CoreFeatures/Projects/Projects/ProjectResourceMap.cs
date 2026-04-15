
using AgileStudioServer.Core.Resources;
using AgileStudioServer.CoreFeatures.Resources.Resource;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    public class ProjectResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.ProjectsProject;
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

        public Type GetResourceServiceType()
        {
            return typeof(ProjectService);
        }
    }
}
