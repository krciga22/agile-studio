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

        public virtual List<Workflow> GetAll()
        {
            List<Entities.Workflow> entities = _DBContext.Workflow.ToList();
            return HydrateWorkflowModels(entities);
        }

        public virtual Workflow? Get(int id)
        {
            Entities.Workflow? entity = _DBContext.Workflow.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateWorkflowModel(entity);
        }

        public virtual Workflow Create(Workflow workflow)
        {
            Entities.Workflow entity = HydrateWorkflowEntity(workflow);

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowModel(entity);
        }

        public virtual Workflow Update(Workflow workflow)
        {
            Entities.Workflow entity = HydrateWorkflowEntity(workflow);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateWorkflowModel(entity);
        }

        public virtual void Delete(Workflow workflow)
        {
            Entities.Workflow entity = HydrateWorkflowEntity(workflow);

            _DBContext.Workflow.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<Workflow> HydrateWorkflowModels(List<Entities.Workflow> entities, int depth = 3)
        {
            List<Workflow> models = new();

            entities.ForEach(entity =>
            {
                Workflow model = HydrateWorkflowModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private Workflow HydrateWorkflowModel(Entities.Workflow workflow, int depth = 3)
        {
            return (Workflow)_Hydrator.Hydrate(
                workflow, typeof(Workflow), depth
            );
        }

        private Entities.Workflow HydrateWorkflowEntity(Workflow workflow, int depth = 3)
        {
            return (Entities.Workflow)_Hydrator.Hydrate(
                workflow, typeof(Entities.Workflow), depth
            );
        }
    }
}
