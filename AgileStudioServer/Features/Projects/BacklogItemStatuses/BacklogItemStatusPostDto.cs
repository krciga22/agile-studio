using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusPostDto
    {
        [Required]
        public int BacklogItemID { get; set; }

        [Required]
        public int WorkflowStateID { get; set; }

        [StringLength(255)]
        public string? Comment { get; set; }

        public BacklogItemStatusPostDto(int backlogItemID, int workflowStateID)
        {
            BacklogItemID = backlogItemID;
            WorkflowStateID = workflowStateID;
        }
    }
}