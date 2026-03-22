using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Resources.Resource;
using AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Sprints.Sprints
{
    public class SprintRepository : EntityRepository<DBContext, SprintModel, Sprint, int>, IResourceRepository
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
            List<Sprint> entities = _DBContext.Sprint.Where(sprint =>
                sprint.Project.ID == projectId).ToList();

            return HydrateModels(entities);
        }

        public int GetNextSprintNumber()
        {
            return GetLastSprintNumber() + 1;
        }

        public string GetResourceType()
        {
            return ResourceTypes.SprintsSprint;
        }

        public Type GetResourceDtoType()
        {
            return typeof(SprintDto);
        }

        public Type GetResourceDtoCreateType()
        {
            return typeof(SprintPostDto);
        }

        public Type GetResourceDtoUpdateType()
        {
            return typeof(SprintPatchDto);
        }

        public Type GetResourceModelType()
        {
            return typeof(SprintModel);
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
            return Create((SprintModel) model);
        }

        public object UpdateResource(int id, object model)
        {
            if (id != GetIdentifier((SprintModel) model))
            {
                throw new ResourceIdentifierMismatchException(id);
            }

            return Update((SprintModel) model);
        }

        public void DeleteResource(object model)
        {
            Delete((SprintModel) model);
        }

        protected override DbSet<Sprint> GetDbSet()
        {
            return _DBContext.Sprint;
        }

        private int GetLastSprintNumber()
        {
            var lastSprint = _DBContext.Sprint
                .OrderByDescending(sprint => sprint.SprintNumber)
                .FirstOrDefault();

            return lastSprint?.SprintNumber ?? 0;
        }
    }
}
