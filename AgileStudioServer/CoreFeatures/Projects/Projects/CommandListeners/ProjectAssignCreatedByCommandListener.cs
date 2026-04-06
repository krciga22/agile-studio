using AgileStudioServer.Core.Command;
using AgileStudioServer.CoreFeatures.Auth.Auth;
using AgileStudioServer.CoreFeatures.Projects.Projects.Commands;

namespace AgileStudioServer.CoreFeatures.Projects.Projects.CommandListeners
{
    public class ProjectAssignCreatedByCommandListener() : ICommandListener
    {
        public Type[] GetEvents()
        {
            return [typeof(ProjectCreateCommand)];
        }

        public int GetPriority()
        {
            return CommandPriority.High;
        }

        public void Handle(ICommand serviceEvent)
        {
            if (serviceEvent is ProjectCreateCommand e){
                if(e.ProjectModel.CreatedByID != null){
                    return;
                }

                CurrentUserClaimsIdentity? currentUserIdentity = null;
                foreach (var identity in e.ServiceContext.currentUser?.Identities ?? []){
                    if (identity is CurrentUserClaimsIdentity){
                        currentUserIdentity = (CurrentUserClaimsIdentity) identity;
                        break;
                    }
                }
                if (currentUserIdentity == null){
                    return;
                }

                e.ProjectModel.CreatedByID = currentUserIdentity.GetUserIdClaimValue() ?? 
                    throw new Exception("User ID claim not found");
            }
        }
    }
}
