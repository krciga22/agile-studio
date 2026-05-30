using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Features.Accounts.AccountTypes
{
    public class AccountTypeModelHydrator(DBContext dbContext) : AbstractModelHydrator(dbContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(AccountType)
            ) && to == typeof(AccountTypeModel);
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
                var accountType = _DBContext.AccountType.Find(from);
                if (accountType != null)
                {
                    from = accountType;
                }
            }

            if (from is AccountType)
            {
                var entity = (AccountType)from;
                model = new AccountTypeModel(entity.Title);
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

            var model = (AccountTypeModel)to;

            if (from is AccountType)
            {
                var entity = (AccountType)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
        }
    }
}