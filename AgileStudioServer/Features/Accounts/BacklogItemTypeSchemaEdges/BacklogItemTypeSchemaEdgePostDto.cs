using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgePostDto
    {
        // todo rename to just SchemaID
        [Required]
        public int BacklogItemTypeSchemaID { get; set; }

        [Required]
        public int? FromTypeID { get; set; }

        [Required]
        public int ToTypeID { get; set; }

        public BacklogItemTypeSchemaEdgePostDto(
            int backlogItemTypeSchemaID, int? fromTypeID, int toTypeID)
        {
            BacklogItemTypeSchemaID = backlogItemTypeSchemaID;
            FromTypeID = fromTypeID;
            ToTypeID = toTypeID;
        }
    }
}
