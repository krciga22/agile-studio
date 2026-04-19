using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Features.Auth.Auth;
using AgileStudioServer.Features.Resources.Resource.Commands;
using System.Reflection;

namespace AgileStudioServer.Features.Resources.Resource.CommandListeners
{
    public class ResourceAssignCreatedByCommandListener() : ICommandListener
    {
        public Type[] GetEvents()
        {
            return [typeof(ResourceCreatedCommand)];
        }

        public int GetPriority()
        {
            return CommandPriority.High;
        }

        public void Handle(ICommand serviceEvent)
        {
            if (serviceEvent is ResourceCreatedCommand e
                && e.Type == ResourceTypes.ProjectsProject)
            {
                PropertyInfo? createdByIDProp = e.Model.GetType().GetProperty("CreatedByID");
                if (createdByIDProp == null){
                    return;
                }

                var createdByIDValue = createdByIDProp.GetValue(e.Model);
                if (createdByIDValue != null){
                    return;
                }

                int? createdByID = GetCurrentUserIdFromServiceContext(e.ServiceContext);
                if (createdByID == null){
                    return;
                }

                createdByIDProp.SetValue(e.Model, createdByID);
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
