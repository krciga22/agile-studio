namespace AgileStudioServer.Core.Command
{
    public class CommandDispatcher(IEnumerable<ICommandHandler> commandHandlers)
    {
        private readonly IEnumerable<ICommandHandler> _CommandHandlers = commandHandlers;

        public ICommandResult Dispatch(ICommand command)
        {
            CommandResult commandResult = new CommandResult();

            List<ICommandHandler> handlers = _CommandHandlers.Where(
                    l => l.GetCommands().Contains(command.GetType())).ToList();

            if(handlers.Count == 0){
                return commandResult;
            }

            handlers = handlers.OrderBy(l => l.GetPriority()).ToList();

            handlers.ForEach(handler => {
                try
                {
                    if (handler.CanHandle(command)) {
                        handler.Handle(command, commandResult);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling command {nameof(command)} with handler {nameof(handler)}: {ex.Message}");
                }
            });

            return commandResult;
        }
    }
}
