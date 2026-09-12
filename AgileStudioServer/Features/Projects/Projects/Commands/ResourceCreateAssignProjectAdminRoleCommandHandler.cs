using AgileStudioServer.Core.Command;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;
using AgileStudioServer.Features.Resources.Resource.Commands;

namespace AgileStudioServer.Features.Projects.Projects.CommandListeners
{
    public class ResourceCreateAssignProjectAdminRoleCommandHandler(
        RoleGrantService roleGrantService
    ) : AbstractCommandHandler
    {
        public override Type[] GetCommands()
        {
            return [typeof(ResourceCreateCommand)];
        }

        public override int GetPriority()
        {
            return CommandPriority.Low;
        }

        public override bool CanHandle(ICommand command)
        {
            return command is ResourceCreateCommand cmd
                && cmd.Type == ResourceTypes.ProjectsProject;
        }

        public override void Handle(ICommand command, ICommandResult result)
        {
            ProjectModel? projectModel = (ProjectModel?) result.GetValue();

            if (projectModel == null || projectModel.CreatedByID == null){
                return;
            }

            var roleGrant = new RoleGrantModel(
                    RoleKeys.PROJECTS_PROJECT_ADMIN,
                    RoleSubjectTypes.USER,
                    projectModel.CreatedByID.Value.ToString(),
                    Scopes.PROJECT)
            {
                ScopeID = projectModel.ID.ToString()
            };
            roleGrantService.Create(roleGrant);
        }
    }
}
