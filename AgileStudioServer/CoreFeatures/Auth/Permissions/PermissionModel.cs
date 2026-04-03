namespace AgileStudioServer.CoreFeatures.Auth.Permissions
{
    public class PermissionModel(string permissionKey, string title)
    {
        public int ID { get; set; }

        public string PermissionKey { get; set; } = permissionKey;

        public string Title { get; set; } = title;

        public string? Description { get; set; }

        public string? Scope { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CreatedByID { get; set; } = null!;
    }
}