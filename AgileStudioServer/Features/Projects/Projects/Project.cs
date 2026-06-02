using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class Project
    {
        public int ID { get; set; }

        public int AccountID { get; set; }

        public Account Account { get; set; } = null!;

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public int BacklogItemTypeSchemaID { get; set; }

        public BacklogItemTypeSchema BacklogItemTypeSchema { get; set; } = null!;

        public int BacklogItemLinkTypeSchemaID { get; set; }

        public BacklogItemLinkTypeSchema BacklogItemLinkTypeSchema { get; set; } = null!;

        public Project(int accountID, string title, int backlogItemTypeSchemaID, int backlogItemLinkTypeSchemaID)
        {
            AccountID = accountID;
            Title = title;
            CreatedOn = DateTime.UtcNow;
            BacklogItemTypeSchemaID = backlogItemTypeSchemaID;
            BacklogItemLinkTypeSchemaID = backlogItemLinkTypeSchemaID;
        }
    }
}