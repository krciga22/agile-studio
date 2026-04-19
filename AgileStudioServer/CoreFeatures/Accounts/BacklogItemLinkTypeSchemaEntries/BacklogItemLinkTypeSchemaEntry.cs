using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries
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