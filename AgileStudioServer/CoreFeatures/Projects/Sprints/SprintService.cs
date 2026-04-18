using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Projects.Sprints
{
    public class SprintService : AbstractService
    {
        private readonly SprintRepository _SprintRepository;

        public SprintService(SprintRepository sprintRepository)
        {
            _SprintRepository = sprintRepository;
        }

        public virtual List<SprintModel> GetByProjectId(int projectId)
        {
            return _SprintRepository.GetByProjectId(projectId);
        }

        public virtual SprintModel? Get(int id)
        {
            return _SprintRepository.Get(id);
        }

        public virtual SprintModel Create(SprintModel sprint)
        {
            return _SprintRepository.Create(sprint);
        }

        public virtual SprintModel Update(SprintModel sprint)
        {
            return _SprintRepository.Update(sprint);
        }

        public virtual void Delete(SprintModel sprint)
        {
            _SprintRepository.Delete(sprint);
        }

        public int GetNextSprintNumber()
        {
            return _SprintRepository.GetNextSprintNumber();
        }
    }
}
