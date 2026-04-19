namespace AgileStudioServer.CoreFeatures.Accounts.ChildBacklogItemTypes
{
    public class ChildBacklogItemTypeModel
    {
        public int ID { get; set; }

        public int ChildTypeID { get; set; }

        public int ParentTypeID { get; set; }

        public int SchemaID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public ChildBacklogItemTypeModel(int childTypeId, int parentTypeId, int schemaId)
        {
            CreatedOn = DateTime.UtcNow;
            ChildTypeID = childTypeId;
            ParentTypeID = parentTypeId;
            SchemaID = schemaId;
        }
    }
}