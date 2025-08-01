using System.Windows.Input;

namespace AgileStudioCLI.Commands
{
    public abstract class AbstractCommand : ICommand
    {
#pragma warning disable CS0067
        public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067

        private string Name = "Untitled Command";

        /// <summary>
        /// Get the name for this command as it would be called from 
        /// the command line.
        /// </summary>
        /// <param name="name"></param>
        public string GetName()
        {
            return Name;
        }

        /// <summary>
        /// Set the name for the command as it would be called from 
        /// the command line.
        /// </summary>
        /// <param name="name"></param>
        protected void SetName(string name)
        {
            Name = name;
        }

        public abstract bool CanExecute(object? parameter);
        public abstract void Execute(object? parameter);
    }
}
