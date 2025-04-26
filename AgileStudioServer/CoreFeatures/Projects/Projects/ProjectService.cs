using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    public class ProjectService : ServiceBase
    {
        private readonly ProjectRepository _ProjectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            _ProjectRepository = projectRepository;
        }

        public virtual List<ProjectModel> GetAll()
        {
            return _ProjectRepository.GetAll();
        }

        public virtual List<ProjectModel> GetByCreatedByUserId(int userId)
        {
            return _ProjectRepository.GetByCreatedByUserId(userId);
        }

        public virtual ProjectModel? Get(int id)
        {
            return _ProjectRepository.Get(id);
        }

        public virtual ProjectModel Create(ProjectModel project)
        {
            return _ProjectRepository.Create(project);
        }

        public virtual ProjectModel Update(ProjectModel project)
        {
            return _ProjectRepository.Update(project);
        }

        public virtual void Delete(ProjectModel project)
        {
            _ProjectRepository.Delete(project);
        }
    }
}
