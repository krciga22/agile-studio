using AgileStudioServer.Core.Resources;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.WorkflowStates
{
    public class WorkflowStateResourceMap : IResourceMap
    {
        public string GetResourceType()
        {
            return ResourceTypes.AccountsWorkflowState;
        }

        public string GetResourcePermissionScope()
        {
            return Scopes.ACCOUNT_WORKFLOW_STATE;
        }

        public Type GetResourceModelType()
        {
            return typeof(WorkflowStateModel);
        }

        public Type GetResourceDtoType()
        {
            return typeof(WorkflowStateDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(WorkflowStatePostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(WorkflowStatePatchDto);
        }

        public Type GetResourceModelServiceType()
        {
            return typeof(WorkflowStateService);
        }

        public ParentScope GetParentResourceScope(Object model)
        {
            return new ParentScope(Scopes.ACCOUNT_WORKFLOW, 
                ((WorkflowStateModel) model).WorkflowId.ToString());
        }

        public bool IsPermissionedResource()
        {
            return true;
        }
    }
}