namespace AgileStudioServer.CoreFeatures.BacklogItems.Services.Models
{
    public class BacklogItemModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public int ProjectID { get; set; }

        public int? SprintID { get; set; } = null;

        public int? ReleaseID { get; set; } = null;

        public int BacklogItemTypeID { get; set; }

        public int WorkflowStateID { get; set; }

        public int? ParentBacklogItemId { get; set; } = null;

        public BacklogItemModel(string title, int projectId, int backlogItemTypeId, int workflowStateId)
        {
            Title = title;
            CreatedOn = DateTime.Now;
            ProjectID = projectId;
            BacklogItemTypeID = backlogItemTypeId;
            WorkflowStateID = workflowStateId;
        }
    }
}