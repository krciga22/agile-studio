using System.ComponentModel.DataAnnotations;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountModel(int accountTypeID)
    {
        public int ID { get; set; }

        [Required]
        public int AccountTypeID { get; set; } = accountTypeID;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int? CreatedByID { get; set; } = null!;
    }
}