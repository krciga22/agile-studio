namespace AgileStudioServer.Features.Resources.Resource.Exceptions
{
    public class ResourceIdentifierMismatchException : Exception
    {
        public ResourceIdentifierMismatchException(object[] id) : base($"The resource identifier ({id}) given doesn't match the given resource")
        {

        }
    }
}
