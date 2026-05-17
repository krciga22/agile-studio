using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.Releases
{
    public class ReleaseService(
        ReleaseRepository releaseRepository,
        ServiceContext serviceContext) : AbstractModelService<ReleaseModel, int>
    {
        private readonly ReleaseRepository _releaseRepository = releaseRepository;

        private readonly ServiceContext _ServiceContext = serviceContext;

        public virtual PaginationResults<ReleaseModel> GetByProjectId(int projectId)
        {
            return _releaseRepository.GetByProjectId(projectId, _ServiceContext);
        }

        public override PaginationResults<ReleaseModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<ReleaseModel> GetSubCollection(String parentResourceType, Object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.ProjectsProject:
                    return GetByProjectId(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        public override ReleaseModel Get(int id)
        {
            return _releaseRepository.Get(id) ?? 
                throw new ModelNotFoundException(nameof(ReleaseModel), id.ToString());
        }

        public override ReleaseModel Create(ReleaseModel release)
        {
            return _releaseRepository.Create(release);
        }

        public override ReleaseModel Update(ReleaseModel release)
        {
            return _releaseRepository.Update(release);
        }

        public override void Delete(ReleaseModel release)
        {
            _releaseRepository.Delete(release);
        }

        public override int GetIdentifier(ReleaseModel release)
        {
            return release.ID;
        }
    }
}
