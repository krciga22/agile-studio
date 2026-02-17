using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.CoreFeatures.Workflows.Workflows
{
    public class WorkflowPatchDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public WorkflowPatchDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
