namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public BacklogItemLinkTypeSchemaModel(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}