using AgileStudioServer.CoreFeatures.Auth.Permissions;
using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Auth.RolePermissions
{
    public class RolePermission(int roleID, int permissionID)
    {
        public int ID { get; set; }

        public int RoleID { get; set; } = roleID;

        public Role Role { get; set; } = null!;

        public int PermissionID { get; set; } = permissionID;

        public Permission Permission { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;
    }
}