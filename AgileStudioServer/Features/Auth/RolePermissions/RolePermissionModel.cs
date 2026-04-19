namespace AgileStudioServer.Features.Auth.RolePermissions
{
    public class RolePermissionModel(string roleKey, string permissionKey)
    {
        public int ID { get; set; }

        public string RoleKey { get; set; } = roleKey;

        public string PermissionKey { get; set; } = permissionKey;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; } = null!;

        public bool IsSystemRolePermission { get; set; } = false;
    }
}