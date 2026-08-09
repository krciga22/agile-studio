namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusModel
    {
        public int ID { get; set; }

        public int BacklogItemID { get; set; }

        public int WorkflowStateID { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public string? Comment { get; set; }

        public BacklogItemStatusModel(int backlogItemID, int workflowStateID)
        {
            BacklogItemID = backlogItemID;
            WorkflowStateID = workflowStateID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}