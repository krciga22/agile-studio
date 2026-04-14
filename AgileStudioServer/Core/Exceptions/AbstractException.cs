namespace AgileStudioServer.Core.Exceptions
{
    public abstract class AbstractException : Exception
    {
        public AbstractException(string? message) : base(message)
        {

        }

        public AbstractException(string? message, Exception? innerException) : 
            base(message, innerException)
        {

        }
    }
}
