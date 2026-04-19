namespace AgileStudioServer.Features.Auth.Roles
{
    public class RoleModel(string roleKey, string title)
    {
        public string RoleKey { get; set; } = roleKey;

        public string Title { get; set; } = title;

        public string? Description { get; set; }

        public string? Scope { get; set; }

        public string? ScopeID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; } = null!;

        public bool IsSystemRole { get; set; } = false;
    }
}