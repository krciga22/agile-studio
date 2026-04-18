using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemLinkTypeSchemaPostDto(string title)
        {
            Title = title;
        }
    }
}
