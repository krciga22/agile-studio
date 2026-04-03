namespace AgileStudioServer.CoreFeatures.Auth.RolePermissions
{
    public class RolePermissionModel(string roleKey, string permissionKey)
    {
        public int ID { get; set; }

        public string RoleKey { get; set; } = roleKey;

        public string PermissionKey { get; set; } = permissionKey;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CreatedByID { get; set; } = null!;
    }
}