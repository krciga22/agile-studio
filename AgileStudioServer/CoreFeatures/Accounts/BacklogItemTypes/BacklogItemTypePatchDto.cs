using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypes
{
    public class BacklogItemTypePatchDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemTypePatchDto(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
