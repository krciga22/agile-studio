using AgileStudioServer.Data;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.Workflows.Services.Models;
using Entities = AgileStudioServer.CoreFeatures.Workflows.Repositories.Entities;

namespace AgileStudioServer.CoreFeatures.Workflows.Services
{
    public class WorkflowStateService
    {
        private DBContext _DBContext;
        private Hydrator _Hydrator;

        public WorkflowStateService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<WorkflowState> GetByWorkflowId(int workflowId)
        {
            List<Entities.WorkflowState> entities = _DBContext.WorkflowState.
                Where(x => x.Workflow.ID == workflowId).ToList();

            return HydrateWorkflowStateModels(entities);
        }

        public virtual WorkflowState? Get(int id)
        {
            Entities.WorkflowState? entity = _DBContext.WorkflowState.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateWorkflowStateModel(entity);
        }

        public virtual WorkflowState Create(WorkflowState workflowState)
        {
            Entities.WorkflowState entity = HydrateWorkflowStateEntity(workflowState);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowStateModel(entity);
        }

        public virtual WorkflowState Update(WorkflowState workflowState)
        {
            Entities.WorkflowState entity = HydrateWorkflowStateEntity(workflowState);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowStateModel(entity);
        }

        public virtual void Delete(WorkflowState workflowState)
        {
            Entities.WorkflowState entity = HydrateWorkflowStateEntity(workflowState);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<WorkflowState> HydrateWorkflowStateModels(List<Entities.WorkflowState> entities, int depth = 3)
        {
            List<WorkflowState> models = new();

            entities.ForEach(entity =>
            {
                WorkflowState model = HydrateWorkflowStateModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private WorkflowState HydrateWorkflowStateModel(Entities.WorkflowState workflowState, int depth = 3)
        {
            return (WorkflowState)_Hydrator.Hydrate(
                workflowState, typeof(WorkflowState), depth
            );
        }

        private Entities.WorkflowState HydrateWorkflowStateEntity(WorkflowState workflowState, int depth = 3)
        {
            return (Entities.WorkflowState)_Hydrator.Hydrate(
                workflowState, typeof(Entities.WorkflowState), depth
            );
        }
    }
}
