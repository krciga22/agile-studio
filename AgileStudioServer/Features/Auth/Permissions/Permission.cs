using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Auth.Permissions
{
    public class Permission(string PermissionKey, string title)
    {
        public string PermissionKey { get; set; } = PermissionKey;

        public string Title { get; set; } = title;

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public bool IsSystemPermission { get; set; } = false;
    }
}