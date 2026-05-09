namespace AgileStudioServer.Features.Auth.RoleGrants
{
    public class RoleGrantModel(string roleKey, string subjectType, string subjectID, string scope = Scopes.Scopes.GLOBAL)
    {
        public int ID { get; set; }

        public string RoleKey { get; set; } = roleKey;

        public string SubjectType { get; set; } = subjectType;

        public string SubjectID { get; set; } = subjectID;

        public string Scope { get; set; } = scope;

        public string? ScopeID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; }
    }
}