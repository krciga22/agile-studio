using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaPatchDto
    {
        [Required]
        public int ID;

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemLinkTypeSchemaPatchDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
