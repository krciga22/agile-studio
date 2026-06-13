namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedById { get; set; } = null!;

        public int AccountID { get; set; }

        public BacklogItemTypeSchemaModel(string title, int accountID)
        {
            Title = title;
            CreatedOn = DateTime.UtcNow;
            AccountID = accountID;
        }
    }
}