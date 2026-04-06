using AgileStudioServer.Core.Command;
using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.CoreFeatures.Auth.RoleGrants;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Projects.Projects.Commands;

namespace AgileStudioServer.CoreFeatures.Projects.Projects.CommandListeners
{
    public class ProjectAssignProjectAdminCommandListener(RoleGrantService roleGrantService) : ICommandListener
    {
        public Type[] GetEvents()
        {
            return [typeof(ProjectCreateCommand)];
        }

        public int GetPriority()
        {
            return CommandPriority.Low;
        }

        public void Handle(ICommand serviceEvent)
        {
            if (serviceEvent is ProjectCreateCommand e)
            {
                ProjectModel? projectModel = e.Result;
                if (projectModel != null && projectModel.CreatedByID != null)
                {
                    var roleGrant = new RoleGrantModel(
                        RoleKeys.PROJECTS_PROJECT_ADMIN,
                        RoleSubjectTypes.USER,
                        projectModel.CreatedByID.Value.ToString())
                    {
                        Scope = PermissionScopes.PROJECTS,
                        ScopeID = projectModel.ID.ToString()
                    };
                    roleGrantService.Create(roleGrant);
                }
            }
        }
    }
}
