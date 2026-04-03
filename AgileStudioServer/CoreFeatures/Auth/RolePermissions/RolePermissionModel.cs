namespace AgileStudioServer.CoreFeatures.Auth.RolePermissions
{
    public class RolePermissionModel(string roleKey, int permissionID)
    {
        public int ID { get; set; }

        public string RoleKey { get; set; } = roleKey;

        public int PermissionID { get; set; } = permissionID;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CreatedByID { get; set; } = null!;
    }
}