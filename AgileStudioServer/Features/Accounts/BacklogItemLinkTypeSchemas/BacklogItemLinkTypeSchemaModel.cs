namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public int AccountID { get; set; }

        public BacklogItemLinkTypeSchemaModel(string title, int accountID)
        {
            Title = title;
            CreatedOn = DateTime.UtcNow;
            AccountID = accountID;
        }
    }
}