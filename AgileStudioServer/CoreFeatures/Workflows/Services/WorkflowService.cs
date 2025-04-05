using AgileStudioServer.Core.Hydrator;
using Entities = AgileStudioServer.CoreFeatures.Workflows.Repositories.Entities;
using AgileStudioServer.CoreFeatures.Workflows.Services.Models;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Workflows.Services
{
    public class WorkflowService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public WorkflowService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<WorkflowModel> GetAll()
        {
            List<Entities.Workflow> entities = _DBContext.Workflow.ToList();
            return HydrateWorkflowModels(entities);
        }

        public virtual WorkflowModel? Get(int id)
        {
            Entities.Workflow? entity = _DBContext.Workflow.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateWorkflowModel(entity);
        }

        public virtual WorkflowModel Create(WorkflowModel workflow)
        {
            Entities.Workflow entity = HydrateWorkflowEntity(workflow);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowModel(entity);
        }

        public virtual WorkflowModel Update(WorkflowModel workflow)
        {
            Entities.Workflow entity = HydrateWorkflowEntity(workflow);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowModel(entity);
        }

        public virtual void Delete(WorkflowModel workflow)
        {
            Entities.Workflow entity = HydrateWorkflowEntity(workflow);

            _DBContext.Workflow.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<WorkflowModel> HydrateWorkflowModels(List<Entities.Workflow> entities, int depth = 3)
        {
            List<WorkflowModel> models = new();

            entities.ForEach(entity =>
            {
                WorkflowModel model = HydrateWorkflowModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private WorkflowModel HydrateWorkflowModel(Entities.Workflow workflow, int depth = 3)
        {
            return (WorkflowModel)_Hydrator.Hydrate(
                workflow, typeof(WorkflowModel), depth
            );
        }

        private Entities.Workflow HydrateWorkflowEntity(WorkflowModel workflow, int depth = 3)
        {
            return (Entities.Workflow)_Hydrator.Hydrate(
                workflow, typeof(Entities.Workflow), depth
            );
        }
    }
}
