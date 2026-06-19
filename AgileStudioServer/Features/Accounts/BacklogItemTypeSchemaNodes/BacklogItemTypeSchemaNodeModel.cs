namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeModel
    {
        public int ID { get; set; }

        public int SchemaID { get; set; }

        public int BacklogItemTypeID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public BacklogItemTypeSchemaNodeModel(int schemaID, int backlogItemTypeID)
        {
            SchemaID = schemaID;
            BacklogItemTypeID = backlogItemTypeID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}