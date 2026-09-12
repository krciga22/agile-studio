
namespace AgileStudioServer.Core.Command
{
    public abstract class AbstractCommandHandler : ICommandHandler
    {
        public abstract Type[] GetCommands();

        public virtual int GetPriority()
        {
            return CommandPriority.Normal;
        }

        public virtual bool CanHandle(ICommand command)
        {
            return GetCommands().Contains(command.GetType());
        }

        public abstract void Handle(ICommand command, ICommandResult result);
    }
}
