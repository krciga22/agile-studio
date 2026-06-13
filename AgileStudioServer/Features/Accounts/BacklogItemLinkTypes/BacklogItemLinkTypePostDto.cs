using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypePostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string TitleOpposite { get; set; }

        [Required]
        public int AccountId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public BacklogItemLinkTypePostDto(string title, string titleOpposite, int accountId)
        {
            Title = title;
            TitleOpposite = titleOpposite;
            AccountId = accountId;
        }
    }
}
