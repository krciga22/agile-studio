namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypeModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public int AccountID { get; set; }

        public int WorkflowID { get; set; }

        public BacklogItemTypeModel(string title, int accountId, int workflowId)
        {
            Title = title;
            CreatedOn = DateTime.UtcNow;
            AccountID = accountId;
            WorkflowID = workflowId;
        }
    }
}