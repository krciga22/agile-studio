using AgileStudioServer.Features.Accounts.Accounts;
using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaPostDto
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required]
        public int AccountID { get; set; }

        public BacklogItemLinkTypeSchemaPostDto(string title, int accountID)
        {
            Title = title;
            AccountID = accountID;
        }
    }
}
