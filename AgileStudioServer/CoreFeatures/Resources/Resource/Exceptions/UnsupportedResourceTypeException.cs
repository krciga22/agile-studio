namespace AgileStudioServer.CoreFeatures.Resources.Resource.Exceptions
{
    public class UnsupportedResourceTypeException : Exception
    {
        public UnsupportedResourceTypeException(string type) : base($"Resource type '{type}' is not supported")
        {

        }
    }
}
