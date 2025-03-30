namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs
{
    public class BacklogItemLinkTypeSchemaSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public BacklogItemLinkTypeSchemaSummaryDto(
            int id,
            string title)
        {
            ID = id;
            Title = title;
        }
    }
}
