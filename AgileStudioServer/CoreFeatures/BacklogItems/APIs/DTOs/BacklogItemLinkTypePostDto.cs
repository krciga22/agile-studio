using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs
{
    public class BacklogItemLinkTypePostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string TitleOpposite { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemLinkTypePostDto(string title, string titleOpposite)
        {
            Title = title;
            TitleOpposite = titleOpposite;
        }
    }
}
