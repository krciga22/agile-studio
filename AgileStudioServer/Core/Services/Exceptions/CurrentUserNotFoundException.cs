namespace AgileStudioServer.Core.Services.Exceptions
{
    public class CurrentUserNotFoundException : Exception
    {
        public CurrentUserNotFoundException()
            : base("Current user ID not found in service context") { }
    }
}