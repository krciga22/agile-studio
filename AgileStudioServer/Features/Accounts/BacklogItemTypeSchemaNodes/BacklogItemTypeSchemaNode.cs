using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNode
    {
        public int ID { get; set; }

        public int SchemaID { get; set; }

        public BacklogItemTypeSchema Schema { get; set; } = null!;

        public int BacklogItemTypeID { get; set; }

        public BacklogItemType BacklogItemType { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public BacklogItemTypeSchemaNode(int schemaID, int backlogItemTypeID)
        {
            SchemaID = schemaID;
            BacklogItemTypeID = backlogItemTypeID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}