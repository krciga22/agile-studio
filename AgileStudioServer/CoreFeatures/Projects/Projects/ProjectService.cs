using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Projects.Projects
{
    public class ProjectService : AbstractService
    {
        private readonly ProjectRepository _ProjectRepository;

        public ProjectService(ProjectRepository projectRepository)
        {
            _ProjectRepository = projectRepository;
        }

        public virtual PaginationResults<ProjectModel> GetAll(ServiceContext serviceContext)
        {
            return _ProjectRepository.GetAll(serviceContext);
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
