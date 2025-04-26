
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Workflows.WorkflowStates
{
    public class WorkflowStateService : ServiceBase
    {
        private WorkflowStateRepository _WorkflowStateRepository;

        public WorkflowStateService(WorkflowStateRepository workflowStateRepository)
        {
            _WorkflowStateRepository = workflowStateRepository;
        }

        public virtual List<WorkflowStateModel> GetByWorkflowId(int workflowId)
        {
            return _WorkflowStateRepository.GetByWorkflowId(workflowId);
        }

        public virtual WorkflowStateModel? Get(int id)
        {
            return _WorkflowStateRepository.Get(id);
        }

        public virtual WorkflowStateModel Create(WorkflowStateModel workflowState)
        {
            return _WorkflowStateRepository.Create(workflowState);
        }

        public virtual WorkflowStateModel Update(WorkflowStateModel workflowState)
        {
            return _WorkflowStateRepository.Update(workflowState);
        }

        public virtual void Delete(WorkflowStateModel workflowState)
        {
            _WorkflowStateRepository.Delete(workflowState);
        }
    }
}
