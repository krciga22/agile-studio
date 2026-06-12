using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Features.Accounts.AccountTypes
{
    public class AccountTypeRepository : EntityRepository<DBContext, AccountTypeModel, AccountType, int>
    {
        public AccountTypeRepository(DBContext dbContext, Hydrator hydrator) : base(dbContext, hydrator)
        {

        }

        public override int GetIdentifier(AccountTypeModel model)
        {
            return model.ID;
        }

        protected override DbSet<AccountType> GetDbSet()
        {
            return _DBContext.AccountType;
        }

        public virtual List<AccountTypeModel> GetAll()
        {
            List<AccountType> entities = _DBContext.AccountType.ToList();

            return HydrateModels(entities);
        }
    }
}