namespace AgileStudioServer.CoreFeatures.Auth.Permissions
{
    public class PermissionModel(string title)
    {
        public int ID { get; set; }

        public string Title { get; set; } = title;

        public string? Description { get; set; }

        public string? Scope { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CreatedByID { get; set; } = null!;
    }
}