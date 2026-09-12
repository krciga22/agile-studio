namespace AgileStudioServer.Core.Command
{
    public class CommandResult(object? value = null) : ICommandResult
    {
        public object? GetValue()
        {
            return _Value;
        }

        public void SetValue(object? value)
        {
            _Value = value;
        }

        private object? _Value = value;
    }
}
