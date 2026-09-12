using AgileStudioServer.Core.Command;
using AgileStudioServer.Features.Resources.Resource;
using AgileStudioServer.Features.Resources.Resource.Commands;

namespace AgileStudioServer.Features.Projects.Sprints.Commands
{
    public class ResourceCreateAssignSprintNumberCommandHandler(
        SprintRepository SprintRepository
    ) : AbstractCommandHandler
    {
        public override Type[] GetCommands()
        {
            return [typeof(ResourceCreateCommand)];
        }

        public override int GetPriority()
        {
            return CommandPriority.High;
        }

        public override bool CanHandle(ICommand command)
        {
            return command is ResourceCreateCommand cmd
                && cmd.Type == ResourceTypes.SprintsSprint;
        }

        public override void Handle(ICommand command, ICommandResult result)
        {
            ResourceCreateCommand cmd = (ResourceCreateCommand)command;
            SprintModel? sprintModel = (SprintModel?)cmd.Model;

            if (sprintModel == null || sprintModel.SprintNumber != 0){
                return;
            }

            sprintModel.SprintNumber = SprintRepository.GetNextSprintNumber();
        }
    }
}
