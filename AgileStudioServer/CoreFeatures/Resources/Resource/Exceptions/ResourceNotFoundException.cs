namespace AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string type, object[] id) : base($"Resource type '{type}' with id '{id}' not found")
        {

        }
    }
}
