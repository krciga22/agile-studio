
namespace AgileStudioServer.CoreFeatures.Auth.APIs.DTOs
{
    public class CurrentUserDto
    {
        public string Name { get; set; }

        public CurrentUserDto(string name)
        {
            Name = name;
        }
    }
}
