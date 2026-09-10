using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsWorkflow;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT_WORKFLOW;
        }

        public Type GetResourceModelType()
        {
            return typeof(WorkflowModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(WorkflowDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(WorkflowPostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(WorkflowPatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(WorkflowService);
        }

        public Type? GetResourceModelRepositoryType()
        {
            return typeof(WorkflowRepository);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.ACCOUNT, 
                ((WorkflowModel) model).AccountID.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}