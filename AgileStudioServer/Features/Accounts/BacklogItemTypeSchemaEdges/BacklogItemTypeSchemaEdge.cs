using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdge
    {
        public int ID { get; set; }

        public int SchemaID { get; set; }

        public BacklogItemTypeSchema Schema { get; set; } = null!;

        public int? FromTypeID { get; set; } = null;

        public BacklogItemType? FromType { get; set; } = null;

        public int ToTypeID { get; set; }

        public BacklogItemType ToType { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public BacklogItemTypeSchemaEdge(int schemaID, int? fromTypeID, int toTypeID)
        {
            SchemaID = schemaID;
            FromTypeID = fromTypeID;
            ToTypeID = toTypeID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}