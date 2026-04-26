using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Roles;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectService : AbstractModelService<ProjectModel, int>
    {
        private readonly ProjectRepository _ProjectRepository;
        private readonly ServiceContext _ServiceContext;
        private readonly RoleGrantService _RoleGrantService;

        public ProjectService(
            ProjectRepository projectRepository, 
            ServiceContext serviceContext,
            RoleGrantService roleGrantService)
        {
            _ProjectRepository = projectRepository;
            _ServiceContext = serviceContext;
            _RoleGrantService = roleGrantService;
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
            int? createdById = _ServiceContext.GetCurrentUserId();
            if(createdById != null){
                model.CreatedByID = createdById;
            }

            ProjectModel project = _ProjectRepository.Create(model);
            if(project.CreatedByID != null)
            {
                var roleGrant = new RoleGrantModel(
                    RoleKeys.PROJECTS_PROJECT_ADMIN,
                    RoleSubjectTypes.USER,
                    project.CreatedByID.Value.ToString()
                )
                {
                    Scope = PermissionScopes.PROJECTS,
                    ScopeID = project.ID.ToString(),
                    CreatedByID = project.CreatedByID
                };
                   
                _RoleGrantService.Create(roleGrant);
            }

            return project;
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
