namespace AgileStudioServer.CoreFeatures.Accounts.Workflows
{
    public class WorkflowModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedById { get; set; } = null!;

        public WorkflowModel(string title)
        {
            Title = title;
            CreatedOn = DateTime.Now;
        }
    }
}