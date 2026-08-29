using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowPatchDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int DefaultWorkflowStateID { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public WorkflowPatchDto(int id, string title, int defaultWorkflowStateID)
        {
            ID = id;
            Title = title;
            DefaultWorkflowStateID = defaultWorkflowStateID;
        }
    }
}
