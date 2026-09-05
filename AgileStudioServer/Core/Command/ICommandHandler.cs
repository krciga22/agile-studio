namespace AgileStudioServer.Core.Command
{
    public interface ICommandHandler
    {
        /// <summary>
        /// Gets the commands handled by this command handler.
        /// </summary>
        Type[] GetCommands();

        /// <summary>
        /// Gets the priority of the handler.
        /// </summary>
        /// <returns>
        /// An integer representing the priority of this 
        /// command handler. Default is 0. Handlers with 
        /// lower values will be executed before those 
        /// with higher values.
        /// </returns>
        // todo make dynamic depending on the command being handled
        int GetPriority();

        void Handle(ICommand command);
    }
}
