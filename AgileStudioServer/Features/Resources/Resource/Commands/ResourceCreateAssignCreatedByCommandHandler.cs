using AgileStudioServer.Core.Command;
using AgileStudioServer.Core.Services;
using System.Reflection;

namespace AgileStudioServer.Features.Resources.Resource.Commands
{
    public class ResourceCreateAssignCreatedByCommandHandler(
        ServiceContext serviceContext) : AbstractCommandHandler
    {
        public override Type[] GetCommands()
        {
            return [typeof(ResourceCreateCommand)];
        }

        public override int GetPriority()
        {
            return CommandPriority.High;
        }

        public override void Handle(ICommand command, ICommandResult result)
        {
            ResourceCreateCommand cmd = (ResourceCreateCommand)command;

            PropertyInfo? createdByIDProp = cmd.Model.GetType().GetProperty("CreatedByID") ??
                cmd.Model.GetType().GetProperty("CreatedById");
            if (createdByIDProp == null){
                return;
            }

            var createdByIDValue = createdByIDProp.GetValue(cmd.Model);
            if (createdByIDValue != null){
                return;
            }

            int? createdByID = serviceContext.GetCurrentUserId();
            if (createdByID == null){
                return;
            }

            createdByIDProp.SetValue(cmd.Model, createdByID);
        }
    }
}
