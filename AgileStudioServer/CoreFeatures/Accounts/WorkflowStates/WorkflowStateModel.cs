using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.CoreFeatures.Accounts.WorkflowStates
{
    public class WorkflowStateModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        [Required]
        public int WorkflowId { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedById { get; set; } = null!;

        public WorkflowStateModel(string title, int workflowId)
        {
            Title = title;
            WorkflowId = workflowId;
            CreatedOn = DateTime.Now;
        }
    }
}