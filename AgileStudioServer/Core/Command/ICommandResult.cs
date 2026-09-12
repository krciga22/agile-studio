
namespace AgileStudioServer.Core.Command
{
    public interface ICommandResult
    {
        public object? GetValue();

        public void SetValue(object? value);
    }
}
