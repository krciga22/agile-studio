using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Auth.Roles
{
    public class Role(string roleKey, string title, string scope = Scopes.Scopes.GLOBAL)
    {
        public string RoleKey { get; set; } = roleKey;

        public string Title { get; set; } = title;

        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the scope in which this role can be applied.
        /// </summary>
        public string Scope { get; set; } = scope;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public bool IsSystemRole { get; set; } = false;
    }
}