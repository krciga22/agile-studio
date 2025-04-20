using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Workflows.Workflows
{
    public class WorkflowRepository : EntityRepository<DBContext, WorkflowModel, Workflow, int>
    {
        public WorkflowRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(WorkflowModel model)
        {
            return model.ID;
        }

        public virtual List<WorkflowModel> GetAll()
        {
            List<Workflow> entities = _DBContext.Workflow.ToList();
            return HydrateModels(entities);
        }

        protected override DbSet<Workflow> GetDbSet()
        {
            return _DBContext.Workflow;
        }
    }
}
