using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypes
{
    public class BacklogItemTypePostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required]
        public int WorkflowId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemTypePostDto(string title, int accountId, int workflowId)
        {
            Title = title;
            AccountId = accountId;
            WorkflowId = workflowId;
        }
    }
}
