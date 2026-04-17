using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Auth.Roles
{
    public class Role(string roleKey, string title)
    {
        public string RoleKey { get; set; } = roleKey;

        public string Title { get; set; } = title;

        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the scope in which this role can be applied.
        /// </summary>
        public string? Scope { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public bool IsSystemRole { get; set; } = false;
    }
}