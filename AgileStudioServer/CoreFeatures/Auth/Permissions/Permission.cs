using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Auth.Permissions
{
    public class Permission(string PermissionKey, string title)
    {
        public string PermissionKey { get; set; } = PermissionKey;

        public string Title { get; set; } = title;

        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the scope in which this permission can be applied.
        /// </summary>
        public string? Scope { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;
    }
}