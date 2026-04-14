using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;
using AgileStudioServer.CoreFeatures.Auth.Auth;
using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.CoreFeatures.Auth.RoleGrants;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Resources.Resource;
using AgileStudioServer.CoreFeatures.Resources.Resource.Commands;

namespace AgileStudioServer.CoreFeatures.Projects.Projects.CommandListeners
{
    public class ProjectAssignProjectAdminRoleCommandListener(RoleGrantService roleGrantService) : ICommandListener
    {
        public Type[] GetEvents()
        {
            return [typeof(ResourceCreatedCommand)];
        }

        public int GetPriority()
        {
            return CommandPriority.Low;
        }

        public void Handle(ICommand serviceEvent)
        {
            if (serviceEvent is ResourceCreatedCommand e 
                && e.Type == ResourceTypes.ProjectsProject)
            {
                ProjectModel projectModel = (ProjectModel) e.Model;

                int? createdBy = GetCurrentUserIdFromServiceContext(e.ServiceContext) ??
                        projectModel.CreatedByID;
                if (createdBy == null){
                    return;
                }

                var roleGrant = new RoleGrantModel(
                        RoleKeys.PROJECTS_PROJECT_ADMIN,
                        RoleSubjectTypes.USER,
                        createdBy.Value.ToString())
                {
                    Scope = PermissionScopes.PROJECTS,
                    ScopeID = projectModel.ID.ToString()
                };
                roleGrantService.Create(roleGrant);
            }
        }

        // todo consolidate this
        public int? GetCurrentUserIdFromServiceContext(ServiceContext serviceContext)
        {
            CurrentUserClaimsIdentity? currentUserIdentity = null;
            foreach (var identity in serviceContext.currentUser?.Identities ?? [])
            {
                if (identity is CurrentUserClaimsIdentity)
                {
                    currentUserIdentity = (CurrentUserClaimsIdentity)identity;
                    break;
                }
            }

            if (currentUserIdentity == null)
            {
                return null;
            }

            var userId = currentUserIdentity.GetUserIdClaimValue();
            if (userId == null){
                throw new Exception("User ID claim not found");
            }

            return userId;
        }
    }
}
