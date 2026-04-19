using AgileStudioServer.Features.Auth.Permissions;
using AgileStudioServer.Features.Auth.Roles;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Auth.RolePermissions
{
    public class RolePermission(string roleKey, string permissionKey)
    {
        public string RoleKey { get; set; } = roleKey;

        public Role Role { get; set; } = null!;

        public string PermissionKey { get; set; } = permissionKey;

        public Permission Permission { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public bool IsSystemRolePermission { get; set; } = false;
    }
}