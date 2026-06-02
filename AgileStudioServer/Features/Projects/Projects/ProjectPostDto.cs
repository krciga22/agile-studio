using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectPostDto
    {
        [Required]
        public int AccountID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int BacklogItemTypeSchemaId { get; set; }

        [Required]
        public int BacklogItemLinkTypeSchemaId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public ProjectPostDto(int accountID, string title, int backlogItemTypeSchemaId, int backlogItemLinkTypeSchemaId)
        {
            AccountID = accountID;
            Title = title;
            BacklogItemTypeSchemaId = backlogItemTypeSchemaId;
            BacklogItemLinkTypeSchemaId = backlogItemLinkTypeSchemaId;
        }
    }
}
