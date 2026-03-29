namespace AgileStudioServer.CoreFeatures.Auth.RolePermissions
{
    public class RolePermissionModel(int roleID, int permissionID)
    {
        public int ID { get; set; }

        public int RoleID { get; set; } = roleID;

        public int PermissionID { get; set; } = permissionID;

        public string? Scope { get; set; }

        public int? ScopeID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CreatedByID { get; set; } = null!;
    }
}