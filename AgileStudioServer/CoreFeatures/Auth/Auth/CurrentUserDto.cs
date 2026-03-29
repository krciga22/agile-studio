namespace AgileStudioServer.CoreFeatures.Auth.Auth
{
    public class CurrentUserDto
    {
        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CurrentUserDto(string fullName, string firstName, string lastName)
        {
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
