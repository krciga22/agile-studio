using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountModelHydrator(DBContext dbContext) : AbstractModelHydrator(dbContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(Account)
            ) && to == typeof(AccountModel);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? model = null;

            if (from is int)
            {
                var account = _DBContext.Account.Find(from);
                if (account != null)
                {
                    from = account;
                }
            }

            if (from is Account)
            {
                var entity = (Account)from;
                model = new AccountModel(entity.AccountTypeID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }

            if (model == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return model;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var model = (AccountModel)to;

            if (from is Account)
            {
                var entity = (Account)from;
                model.ID = entity.ID;
                model.AccountTypeID = entity.AccountTypeID;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
        }
    }
}