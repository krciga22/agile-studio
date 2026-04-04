using AgileStudioServer.CoreFeatures.Auth.Roles;
using AgileStudioServer.CoreFeatures.Users.Users;
using System.Security.Cryptography;

namespace AgileStudioServer.CoreFeatures.Auth.RoleGrants
{
    public class RoleGrant(string roleKey, string subjectType, string subjectID, string? scope, string? scopeID)
    {
        public int ID { get; set; }

        public string RoleKey { get; set; } = roleKey;

        public Role Role { get; set; } = null!;

        /// <summary>
        /// Gets or sets the subject to whom this role is 
        /// being granted.
        /// </summary>
        /// <see cref="RoleSubjectTypes"/>
        public string SubjectType { get; set; } = subjectType;

        /// <summary>
        /// Gets or sets the ID of the subject to whom this 
        /// role is being granted.
        /// </summary>
        public string SubjectID { get; set; } = subjectID;

        /// <summary>
        /// Gets or sets the scope in which this role is 
        /// being granted.
        /// </summary>
        public string? Scope { get; set; } = scope;

        /// <summary>
        /// Gets or sets the ID of the resource for which 
        /// this role is being granted.
        /// </summary>
        public string? ScopeID { get; set; } = scopeID;

        public string Hash { get; set; } = CreateHash(roleKey, subjectType, subjectID, scope, scopeID);

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        private static string CreateHash(string roleKey, string subjectType, string subjectID, string? scope, string? scopeID)
        {
            return Convert.ToHexString(MD5.HashData(System.Text.Encoding.UTF8.GetBytes($"{roleKey}:{subjectType}:{subjectID}:{scope}:{scopeID}")));
        }
    }
}