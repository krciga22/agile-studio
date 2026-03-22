namespace AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions
{
    public class ResourceIdentifierMismatchException : Exception
    {
        public ResourceIdentifierMismatchException(int id) : base($"The resource identifier ({id}) given doesn't match the given resource")
        {

        }
    }
}
