using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.Sprints
{
    public class SprintResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.SprintsSprint;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.PROJECT_SPRINT;
        }

        public Type GetResourceModelType()
        {
            return typeof(SprintModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(SprintDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(SprintPostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(SprintPatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(SprintService);
        }

        public Type? GetResourceModelRepositoryType()
        {
            return typeof(SprintRepository);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            SprintModel sprintModel = ((SprintModel)model);
            return new ParentScope(Scopes.PROJECT, sprintModel.ProjectID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}