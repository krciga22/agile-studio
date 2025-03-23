namespace AgileStudioServer.CoreFeatures.BacklogItems.Services.Models
{
    public class BacklogItemLinkType
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string TitleOpposite { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public BacklogItemLinkType(string title, string titleOpposite)
        {
            Title = title;
            TitleOpposite = titleOpposite;
            CreatedOn = DateTime.Now;
        }
    }
}