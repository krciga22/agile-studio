using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Resources.Resource;
using AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Releases.Releases
{
    public class ReleaseRepository : EntityRepository<DBContext, ReleaseModel, Release, int>, IResourceRepository
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
            List<Release> entities = _DBContext.Release.Where(release =>
                release.Project.ID == projectId).ToList();

            return HydrateModels(entities);
        }

        public string GetResourceType()
        {
            return ResourceTypes.ReleasesRelease;
        }

        public Type GetResourceDtoType()
        {
            return typeof(ReleaseDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(ReleasePostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(ReleasePatchDto);
        }

        public Type GetResourceModelType()
        {
            return typeof(ReleaseModel);
        }

        public PaginationResults<object> GetAllResources(ServiceContext serviceContext)
        {
            throw new NotImplementedException();
        }

        public object? GetResource(int id)
        {
            return Get(id);
        }

        public bool IsResource(int id)
        {
            return Exists(id);
        }

        public object CreateResource(object model)
        {
            return Create((ReleaseModel) model);
        }

        public object UpdateResource(int id, object model)
        {
            if (id != GetIdentifier((ReleaseModel) model))
            {
                throw new ResourceIdentifierMismatchException(id);
            }

            return Update((ReleaseModel) model);
        }

        public void DeleteResource(object model)
        {
            Delete((ReleaseModel) model);
        }

        protected override DbSet<Release> GetDbSet()
        {
            return _DBContext.Release;
        }
    }
}
