using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEntries
{
    public class BacklogItemTypeSchemaEntry
    {
        public int ID { get; set; }

        public int ChildTypeID { get; set; }

        public BacklogItemType ChildType { get; set; } = null!;

        public int ParentTypeID { get; set; }

        public BacklogItemType ParentType { get; set; } = null!;

        public int SchemaID { get; set; }

        public BacklogItemTypeSchema Schema { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public BacklogItemTypeSchemaEntry(int childTypeID, int parentTypeID, int schemaID)
        {
            CreatedOn = DateTime.UtcNow;
            ChildTypeID = childTypeID;
            ParentTypeID = parentTypeID;
            SchemaID = schemaID;
        }
    }
}