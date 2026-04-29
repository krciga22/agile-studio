namespace AgileStudioServer.Features.Resources.Resource.Exceptions
{
    public class ResourceServiceNotFoundException : Exception
    {
        public ResourceServiceNotFoundException(string type) : base($"Resource service for resource type '{type}' not found")
        {

        }
    }
}
