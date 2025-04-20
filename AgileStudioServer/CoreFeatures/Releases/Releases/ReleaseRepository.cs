using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Releases.Releases
{
    public class ReleaseRepository : EntityRepository<DBContext, ReleaseModel, Release, int>
    {
        public ReleaseRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(ReleaseModel model)
        {
            return model.ID;
        }

        public virtual List<ReleaseModel> GetByProjectId(int projectId)
        {
            List<Release> entities = _DbContext.Release.Where(release =>
                release.Project.ID == projectId).ToList();

            return HydrateModels(entities);
        }

        protected override DbSet<Release> GetDbSet()
        {
            return _DbContext.Release;
        }
    }
}
