using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodePostDto
    {
        [Required]
        public int BacklogItemTypeSchemaID { get; set; }

        [Required]
        public int BacklogItemTypeID { get; set; }

        public BacklogItemTypeSchemaNodePostDto(
            int backlogItemTypeSchemaID, int backlogItemTypeID)
        {
            BacklogItemTypeSchemaID = backlogItemTypeSchemaID;
            BacklogItemTypeID = backlogItemTypeID;
        }
    }
}
