using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectService : AbstractModelService<ProjectModel, int>
    {
        private readonly ProjectRepository _ProjectRepository;
        private readonly ServiceContext _ServiceContext;

        public ProjectService(ProjectRepository projectRepository, ServiceContext serviceContext)
        {
            _ProjectRepository = projectRepository;
            _ServiceContext = serviceContext;
        }

        public override PaginationResults<ProjectModel> GetCollection()
        {
            return _ProjectRepository.GetAll(_ServiceContext);
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public override ProjectModel Get(int id)
        {
            var project = _ProjectRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(ProjectModel), id.ToString());

            return project;
        }

        public override ProjectModel Create(ProjectModel model)
        {
            return _ProjectRepository.Create(model);
        }

        public override ProjectModel Update(ProjectModel model)
        {
            return _ProjectRepository.Update(model);
        }

        public override void Delete(ProjectModel model)
        {
            _ProjectRepository.Delete(model);
        }
    }
}
