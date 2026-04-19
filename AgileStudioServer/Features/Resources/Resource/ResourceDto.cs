namespace AgileStudioServer.Features.Resources.Resource
{
    public class ResourceDto{
        public string Type { get; set; }

        public object Resource { get; set; }

        public ResourceDto(string type, object resource)
        {
            Type = type;
            Resource = resource;
        }
    }
}
