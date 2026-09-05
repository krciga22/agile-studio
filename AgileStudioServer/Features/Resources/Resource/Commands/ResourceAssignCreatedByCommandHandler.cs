using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Features.Auth.Auth;
using System.Reflection;

namespace AgileStudioServer.Features.Resources.Resource.Commands
{
    public class ResourceAssignCreatedByCommandHandler() : ICommandHandler
    {
        public Type[] GetCommands()
        {
            return [typeof(ResourceCreateCommand)];
        }

        public int GetPriority()
        {
            return CommandPriority.High;
        }

        public void Handle(ICommand command)
        {
            if (command is ResourceCreateCommand c
                && c.Type == ResourceTypes.ProjectsProject)
            {
                PropertyInfo? createdByIDProp = c.Model.GetType().GetProperty("CreatedByID");
                if (createdByIDProp == null){
                    return;
                }

                var createdByIDValue = createdByIDProp.GetValue(c.Model);
                if (createdByIDValue != null){
                    return;
                }

                int? createdByID = GetCurrentUserIdFromServiceContext(c.ServiceContext);
                if (createdByID == null){
                    return;
                }

                createdByIDProp.SetValue(c.Model, createdByID);
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
            if (userId == null)
            {
                throw new Exception("User ID claim not found");
            }

            return userId;
        }
    }
}
