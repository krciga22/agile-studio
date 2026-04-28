namespace AgileStudioServer.Features.Resources.Resource.Exceptions
{
    public class ResourceServiceNotFound : Exception
    {
        public ResourceServiceNotFound(string type) : base($"Resource service for resource type '{type}' not found")
        {

        }
    }
}
