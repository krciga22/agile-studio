namespace AgileStudioServer.Core.Exceptions
{
    public abstract class AbstractException : Exception
    {
        public AbstractException(string? message) : base(message)
        {

        }
    }
}
