using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int AccountId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public WorkflowPostDto(string title, int accountId)
        {
            Title = title;
            AccountId = accountId;
        }
    }
}
