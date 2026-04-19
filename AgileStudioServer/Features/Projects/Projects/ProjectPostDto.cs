using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int BacklogItemTypeSchemaId { get; set; }

        [Required]
        public int BacklogItemLinkTypeSchemaId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public ProjectPostDto(string title, int backlogItemTypeSchemaId, int backlogItemLinkTypeSchemaId)
        {
            Title = title;
            BacklogItemTypeSchemaId = backlogItemTypeSchemaId;
            BacklogItemLinkTypeSchemaId = backlogItemLinkTypeSchemaId;
        }
    }
}
