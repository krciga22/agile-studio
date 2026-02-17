using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypePatchDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string TitleOpposite { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemLinkTypePatchDto(int id, string title, string titleOpposite)
        {
            ID = id;
            Title = title;
            TitleOpposite = titleOpposite;
        }
    }
}
