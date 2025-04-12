namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryModel
    {
        public int ID { get; set; }

        public int BacklogItemLinkTypeSchemaID { get; set; }

        public int BacklogItemLinkTypeID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public BacklogItemLinkTypeSchemaEntryModel(int backlogItemLinkTypeSchemaID, int backlogItemLinkTypeID)
        {
            BacklogItemLinkTypeSchemaID = backlogItemLinkTypeSchemaID;
            BacklogItemLinkTypeID = backlogItemLinkTypeID;
            CreatedOn = DateTime.Now;
        }
    }
}