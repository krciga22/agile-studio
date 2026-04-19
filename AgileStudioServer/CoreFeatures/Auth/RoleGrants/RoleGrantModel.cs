namespace AgileStudioServer.CoreFeatures.Auth.RoleGrants
{
    public class RoleGrantModel(string roleKey, string subjectType, string subjectID)
    {
        public int ID { get; set; }

        public string RoleKey { get; set; } = roleKey;

        public string SubjectType { get; set; } = subjectType;

        public string SubjectID { get; set; } = subjectID;

        public string? Scope { get; set; }

        public string? ScopeID { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; }
    }
}