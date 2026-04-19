using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryPostDto
    {
        [Required]
        public int BacklogItemLinkTypeSchemaID { get; set; }

        [Required]
        public int BacklogItemLinkTypeID { get; set; }

        public BacklogItemLinkTypeSchemaEntryPostDto(int backlogItemLinkTypeSchemaID, int backlogItemLinkTypeID)
        {
            BacklogItemLinkTypeSchemaID = backlogItemLinkTypeSchemaID;
            BacklogItemLinkTypeID = backlogItemLinkTypeID;
        }
    }
}
