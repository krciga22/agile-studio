using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Releases.Releases
{
    public class ReleaseService : ServiceBase
    {
        private readonly ReleaseRepository _releaseRepository;

        public ReleaseService(ReleaseRepository releaseRepository)
        {
            _releaseRepository = releaseRepository;
        }

        public virtual List<ReleaseModel> GetByProjectId(int projectId)
        {
            return _releaseRepository.GetByProjectId(projectId);
        }

        public virtual ReleaseModel? Get(int id)
        {
            return _releaseRepository.Get(id);
        }

        public virtual ReleaseModel Create(ReleaseModel release)
        {
            return _releaseRepository.Create(release);
        }

        public virtual ReleaseModel Update(ReleaseModel release)
        {
            return _releaseRepository.Update(release);
        }

        public virtual void Delete(ReleaseModel release)
        {
            _releaseRepository.Delete(release);
        }
    }
}
