using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Features.Auth.Auth;
using AgileStudioServer.Features.Auth.RoleGrants;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Auth.Scopes;
using AgileStudioServer.Features.Resources.Resource;
using AgileStudioServer.Features.Resources.Resource.Commands;

namespace AgileStudioServer.Features.Projects.Projects.CommandListeners
{
    public class ProjectAssignProjectAdminRoleCommandHandler(RoleGrantService roleGrantService) : ICommandHandler
    {
        public Type[] GetCommands()
        {
            return [typeof(ResourceCreateCommand)];
        }

        public int GetPriority()
        {
            return CommandPriority.Low;
        }

        public void Handle(ICommand command)
        {
            if (command is ResourceCreateCommand c 
                && c.Type == ResourceTypes.ProjectsProject)
            {
                ProjectModel projectModel = (ProjectModel) c.Model;

                int? createdBy = GetCurrentUserIdFromServiceContext(c.ServiceContext) ??
                        projectModel.CreatedByID;
                if (createdBy == null){
                    return;
                }

                var roleGrant = new RoleGrantModel(
                        RoleKeys.PROJECTS_PROJECT_ADMIN,
                        RoleSubjectTypes.USER,
                        createdBy.Value.ToString(),
                        Scopes.PROJECT)
                {
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
