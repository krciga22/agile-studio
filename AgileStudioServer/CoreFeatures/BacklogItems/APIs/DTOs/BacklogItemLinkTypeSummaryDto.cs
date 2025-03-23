namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs
{
    public class BacklogItemLinkTypeSummaryDto
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string TitleOpposite { get; set; }

        public string? Description { get; set; }

        public BacklogItemLinkTypeSummaryDto(
            int id,
            string title,
            string titleOpposite)
        {
            ID = id;
            Title = title;
            TitleOpposite = titleOpposite;
        }
    }
}
