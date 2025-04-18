using AgileStudioServer.Data;
using AgileStudioServer.Core.Hydrator;

namespace AgileStudioServer.CoreFeatures.Workflows.WorkflowStates
{
    public class WorkflowStateRepository
    {
        private DBContext _DBContext;
        private Hydrator _Hydrator;

        public WorkflowStateRepository(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<WorkflowStateModel> GetByWorkflowId(int workflowId)
        {
            List<WorkflowState> entities = _DBContext.WorkflowState.
                Where(x => x.Workflow.ID == workflowId).ToList();

            return HydrateWorkflowStateModels(entities);
        }

        public virtual WorkflowStateModel? Get(int id)
        {
            WorkflowState? entity = _DBContext.WorkflowState.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateWorkflowStateModel(entity);
        }

        public virtual WorkflowStateModel Create(WorkflowStateModel workflowState)
        {
            WorkflowState entity = HydrateWorkflowStateEntity(workflowState);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowStateModel(entity);
        }

        public virtual WorkflowStateModel Update(WorkflowStateModel workflowState)
        {
            WorkflowState entity = HydrateWorkflowStateEntity(workflowState);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowStateModel(entity);
        }

        public virtual void Delete(WorkflowStateModel workflowState)
        {
            WorkflowState entity = HydrateWorkflowStateEntity(workflowState);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<WorkflowStateModel> HydrateWorkflowStateModels(List<WorkflowState> entities, int depth = 3)
        {
            List<WorkflowStateModel> models = new();

            entities.ForEach(entity =>
            {
                WorkflowStateModel model = HydrateWorkflowStateModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private WorkflowStateModel HydrateWorkflowStateModel(WorkflowState workflowState, int depth = 3)
        {
            return (WorkflowStateModel)_Hydrator.Hydrate(
                workflowState, typeof(WorkflowStateModel), depth
            );
        }

        private WorkflowState HydrateWorkflowStateEntity(WorkflowStateModel workflowState, int depth = 3)
        {
            return (WorkflowState)_Hydrator.Hydrate(
                workflowState, typeof(WorkflowState), depth
            );
        }
    }
}
