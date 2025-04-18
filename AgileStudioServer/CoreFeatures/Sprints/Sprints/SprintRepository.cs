using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Sprints.Sprints
{
    public class SprintRepository : EntityRepository<DBContext, SprintModel, Sprint, int>
    {
        public SprintRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {
        }

        public override int GetIdentifier(SprintModel model)
        {
            return model.ID;
        }

        public virtual List<SprintModel> GetByProjectId(int projectId)
        {
            List<Sprint> entities = _DbContext.Sprint.Where(sprint =>
                sprint.Project.ID == projectId).ToList();

            return HydrateModels(entities);
        }

        public int GetNextSprintNumber()
        {
            return GetLastSprintNumber() + 1;
        }

        protected override DbSet<Sprint> GetDbSet()
        {
            return _DbContext.Sprint;
        }

        private int GetLastSprintNumber()
        {
            var lastSprint = _DbContext.Sprint
                .OrderByDescending(sprint => sprint.SprintNumber)
                .FirstOrDefault();

            return lastSprint?.SprintNumber ?? 0;
        }
    }
}
