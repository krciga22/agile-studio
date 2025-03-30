using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.CoreFeatures.Sprints.Services.Models;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Sprints.Services
{
    public class SprintService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public SprintService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<SprintModel> GetByProjectId(int projectId)
        {
            List<CoreFeatures.Sprints.Repositories.Entities.Sprint> entities = _DBContext.Sprint.Where(sprint =>
                sprint.Project.ID == projectId).ToList();

            return HydrateSprintModels(entities);
        }

        public virtual SprintModel? Get(int id)
        {
            CoreFeatures.Sprints.Repositories.Entities.Sprint? entity = _DBContext.Sprint.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateSprintModel(entity);
        }

        public virtual SprintModel Create(SprintModel sprint)
        {
            CoreFeatures.Sprints.Repositories.Entities.Sprint entity = HydrateSprintEntity(sprint);

            entity.SprintNumber = GetNextSprintNumber();

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateSprintModel(entity);
        }

        public virtual SprintModel Update(SprintModel sprint)
        {
            CoreFeatures.Sprints.Repositories.Entities.Sprint entity = HydrateSprintEntity(sprint);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateSprintModel(entity);
        }

        public virtual void Delete(SprintModel sprint)
        {
            CoreFeatures.Sprints.Repositories.Entities.Sprint entity = HydrateSprintEntity(sprint);

            _DBContext.Remove(entity);
            _DBContext.SaveChanges();
        }

        public int GetNextSprintNumber()
        {
            return GetLastSprintNumber() + 1;
        }

        private int GetLastSprintNumber()
        {
            var lastSprint = _DBContext.Sprint
                .OrderByDescending(sprint => sprint.SprintNumber)
                .FirstOrDefault();

            return lastSprint?.SprintNumber ?? 0;
        }

        private List<SprintModel> HydrateSprintModels(List<CoreFeatures.Sprints.Repositories.Entities.Sprint> entities, int depth = 3)
        {
            List<SprintModel> models = new();

            entities.ForEach(entity =>
            {
                SprintModel model = HydrateSprintModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private SprintModel HydrateSprintModel(CoreFeatures.Sprints.Repositories.Entities.Sprint sprint, int depth = 3)
        {
            return (SprintModel)_Hydrator.Hydrate(
                sprint, typeof(SprintModel), depth
            );
        }

        private CoreFeatures.Sprints.Repositories.Entities.Sprint HydrateSprintEntity(SprintModel sprint, int depth = 3)
        {
            return (CoreFeatures.Sprints.Repositories.Entities.Sprint)_Hydrator.Hydrate(
                sprint, typeof(CoreFeatures.Sprints.Repositories.Entities.Sprint), depth
            );
        }
    }
}
