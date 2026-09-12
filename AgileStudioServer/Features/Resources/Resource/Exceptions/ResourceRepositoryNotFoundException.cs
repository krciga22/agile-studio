namespace AgileStudioServer.Features.Resources.Resource.Exceptions
{
    public class ResourceRepositoryNotFoundException : Exception
    {
        public ResourceRepositoryNotFoundException(string type) : base($"Repository for resource type '{type}' not found")
        {

        }
    }
}
