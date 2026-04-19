using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntry
    {
        public int ID { get; set; }

        public int BacklogItemLinkTypeSchemaID { get; set; }

        public BacklogItemLinkTypeSchema BacklogItemLinkTypeSchema { get; set; } = null!;

        public int BacklogItemLinkTypeID { get; set; }

        public BacklogItemLinkType BacklogItemLinkType { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public BacklogItemLinkTypeSchemaEntry(int backlogItemLinkTypeSchemaID, int backlogItemLinkTypeID)
        {
            BacklogItemLinkTypeSchemaID = backlogItemLinkTypeSchemaID;
            BacklogItemLinkTypeID = backlogItemLinkTypeID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}