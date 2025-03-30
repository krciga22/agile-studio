using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Sprints.Sprints
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
            List<Sprint> entities = _DBContext.Sprint.Where(sprint =>
                sprint.Project.ID == projectId).ToList();

            return HydrateSprintModels(entities);
        }

        public virtual SprintModel? Get(int id)
        {
            Sprint? entity = _DBContext.Sprint.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateSprintModel(entity);
        }

        public virtual SprintModel Create(SprintModel sprint)
        {
            Sprint entity = HydrateSprintEntity(sprint);

            entity.SprintNumber = GetNextSprintNumber();

            _DBContext.Add(entity);
            _DBContext.SaveChanges();

            return HydrateSprintModel(entity);
        }

        public virtual SprintModel Update(SprintModel sprint)
        {
            Sprint entity = HydrateSprintEntity(sprint);

            _DBContext.Update(entity);
            _DBContext.SaveChanges();

            return HydrateSprintModel(entity);
        }

        public virtual void Delete(SprintModel sprint)
        {
            Sprint entity = HydrateSprintEntity(sprint);

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

        private List<SprintModel> HydrateSprintModels(List<Sprint> entities, int depth = 3)
        {
            List<SprintModel> models = new();

            entities.ForEach(entity =>
            {
                SprintModel model = HydrateSprintModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private SprintModel HydrateSprintModel(Sprint sprint, int depth = 3)
        {
            return (SprintModel)_Hydrator.Hydrate(
                sprint, typeof(SprintModel), depth
            );
        }

        private Sprint HydrateSprintEntity(SprintModel sprint, int depth = 3)
        {
            return (Sprint)_Hydrator.Hydrate(
                sprint, typeof(Sprint), depth
            );
        }
    }
}
