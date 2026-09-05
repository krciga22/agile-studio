namespace AgileStudioServer.Core.Command
{
    public class CommandDispatcher(IEnumerable<ICommandHandler> commandHandlers)
    {
        private readonly IEnumerable<ICommandHandler> _CommandHandlers = commandHandlers;

        public void Dispatch(ICommand command)
        {
            List<ICommandHandler> handlers = _CommandHandlers.Where(
                    l => l.GetCommands().Contains(command.GetType())).ToList();

            if(handlers.Count == 0){
                return;
            }

            handlers = handlers.OrderBy(l => l.GetPriority()).ToList();

            handlers.ForEach(handler => {
                try
                {
                    handler.Handle(command);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error handling command {nameof(command)} with handler {nameof(handler)}: {ex.Message}");
                }
            });
        }
    }
}
