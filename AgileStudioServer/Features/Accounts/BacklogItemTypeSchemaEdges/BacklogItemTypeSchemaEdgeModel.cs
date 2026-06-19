namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeModel
    {
        public int ID { get; set; }

        public int SchemaID { get; set; }

        public int FromTypeID { get; set; }

        public int ToTypeID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public BacklogItemTypeSchemaEdgeModel(int schemaId, int fromTypeID, int toTypeID)
        {
            SchemaID = schemaId;
            FromTypeID = fromTypeID;
            ToTypeID = toTypeID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}