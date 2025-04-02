using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Data;
using AgileStudioServer.Migrations;

namespace AgileStudioServer.CoreFeatures.Releases.Releases
{
    public class ReleaseService
    {
        private readonly DBContext _DBContext;

        private readonly Hydrator _Hydrator;

        public ReleaseService(DBContext dbContext, Hydrator hydrator)
        {
            _DBContext = dbContext;
            _Hydrator = hydrator;
        }

        public virtual List<ReleaseModel> GetByProjectId(int projectId)
        {
            List<Release> entities = _DBContext.Release.Where(release =>
                release.Project.ID == projectId).ToList();

            return HydrateReleaseModels(entities);
        }

        public virtual ReleaseModel? Get(int id)
        {
            Release? entity = _DBContext.Release.Find(id);
            if (entity is null)
            {
                return null;
            }

            return HydrateReleaseModel(entity);
        }

        public virtual ReleaseModel Create(ReleaseModel release)
        {
            Release entity = HydrateReleaseEntity(release);

            _DBContext.Release.Add(entity);
            _DBContext.SaveChanges();

            return HydrateReleaseModel(entity);
        }

        public virtual ReleaseModel Update(ReleaseModel release)
        {
            Release entity = HydrateReleaseEntity(release);

            _DBContext.Release.Update(entity);
            _DBContext.SaveChanges();

            return HydrateReleaseModel(entity);
        }

        public virtual void Delete(ReleaseModel release)
        {
            Release entity = HydrateReleaseEntity(release);

            _DBContext.Release.Remove(entity);
            _DBContext.SaveChanges();
        }

        private List<ReleaseModel> HydrateReleaseModels(List<Release> entities, int depth = 3)
        {
            List<ReleaseModel> models = new();

            entities.ForEach(entity =>
            {
                ReleaseModel model = HydrateReleaseModel(entity, depth);
                models.Add(model);
            });

            return models;
        }

        private ReleaseModel HydrateReleaseModel(Release release, int depth = 3)
        {
            return (ReleaseModel)_Hydrator.Hydrate(
                release, typeof(ReleaseModel), depth
            );
        }

        private Release HydrateReleaseEntity(ReleaseModel release, int depth = 3)
        {
            return (Release)_Hydrator.Hydrate(
                release, typeof(Release), depth
            );
        }
    }
}
