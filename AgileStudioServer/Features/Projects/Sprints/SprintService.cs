using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.Sprints
{
    public class SprintService : AbstractModelService<SprintModel, int>
    {
        private readonly SprintRepository _SprintRepository;
        private readonly ServiceContext _ServiceContext;

        public SprintService(
            SprintRepository sprintRepository,
            ServiceContext serviceContext)
        {
            _SprintRepository = sprintRepository;
            _ServiceContext = serviceContext;
        }

        public override PaginationResults<SprintModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<SprintModel> GetSubCollection(String parentResourceType, Object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.ProjectsProject:
                    return GetByProjectId(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public override SprintModel Get(int id)
        {
            return _SprintRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(SprintModel), id.ToString());
        }

        public override SprintModel Create(SprintModel sprint)
        {
            if(sprint.SprintNumber == 0) {
                sprint.SprintNumber = GetNextSprintNumber();
            }

            return _SprintRepository.Create(sprint);
        }

        public override SprintModel Update(SprintModel sprint)
        {
            return _SprintRepository.Update(sprint);
        }

        public override void Delete(SprintModel sprint)
        {
            _SprintRepository.Delete(sprint);
        }

        public override int GetIdentifier(SprintModel sprint)
        {
            return sprint.ID;
        }

        public virtual PaginationResults<SprintModel> GetByProjectId(int projectId)
        {
            return _SprintRepository.GetByProjectId(projectId, _ServiceContext);
        }

        public int GetNextSprintNumber()
        {
            return _SprintRepository.GetNextSprintNumber();
        }
    }
}
