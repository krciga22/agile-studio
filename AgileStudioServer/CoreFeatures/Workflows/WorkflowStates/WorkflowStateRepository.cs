using AgileStudioServer.Data;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Workflows.WorkflowStates
{
    public class WorkflowStateRepository : EntityRepository<DBContext, WorkflowStateModel, WorkflowState, int>
    {
        public WorkflowStateRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(WorkflowStateModel model)
        {
            return model.ID;
        }

        public virtual List<WorkflowStateModel> GetByWorkflowId(int workflowId)
        {
            List<WorkflowState> entities = _DBContext.WorkflowState.
                Where(x => x.Workflow.ID == workflowId).ToList();

            return HydrateModels(entities);
        }

        protected override DbSet<WorkflowState> GetDbSet()
        {
            return _DBContext.WorkflowState;
        }
    }
}
