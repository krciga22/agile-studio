namespace AgileStudioServer.CoreFeatures.Users.Users
{
    public class UserModel
    {
        public int ID { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? AuthServerUserID { get; set; } = null;

        public UserModel(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            CreatedOn = DateTime.UtcNow;
        }
    }
}