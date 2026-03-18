namespace AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string type, int id) : base($"Resource type '{type}' with id '{id}' not found")
        {

        }
    }
}
