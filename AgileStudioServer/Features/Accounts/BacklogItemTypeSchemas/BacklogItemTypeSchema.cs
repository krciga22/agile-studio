using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchema
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int AccountID { get; set; }

        public Account Account { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedByID { get; set; } = null!;

        public User? CreatedBy { get; set; } = null!;

        public BacklogItemTypeSchema(string title, int accountID)
        {
            Title = title;
            AccountID = accountID;
            CreatedOn = DateTime.UtcNow;
        }
    }
}