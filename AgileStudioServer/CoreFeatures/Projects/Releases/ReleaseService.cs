using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;

namespace AgileStudioServer.CoreFeatures.Projects.Releases
{
    public class ReleaseService : AbstractModelService<ReleaseModel, int>
    {
        private readonly ReleaseRepository _releaseRepository;

        public ReleaseService(ReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public virtual List<ReleaseModel> GetByProjectId(int projectId)
        {
            // todo use pagination from service context
            return _releaseRepository.GetByProjectId(projectId);
        }

        public override PaginationResults<ReleaseModel> GetCollection()
        {
            throw new NotImplementedException();
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
    }
}
