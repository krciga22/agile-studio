using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.AccountTypes;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountHydrator(DBContext dBContext) : AbstractEntityHydrator(dBContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(AccountModel)
            ) && to == typeof(Account);
        }

        public override object Hydrate(object from, Type to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is AccountModel)
            {
                var model = (AccountModel)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.Account.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(Account), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new Account(model.AccountTypeID);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.Account.Find(from);
            }

            if (entity == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return entity;
        }

        public override void Hydrate(object from, object to, int maxDepth = 0, int depth = 0, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var entity = (Account)to;
            int nextDepth = depth + 1;

            if (from is AccountModel)
            {
                var model = (AccountModel)from;

                entity.ID = model.ID;
                entity.AccountTypeID = model.AccountTypeID;
                entity.CreatedOn = model.CreatedOn;
                entity.CreatedByID = model.CreatedByID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.AccountType = (AccountType)referenceHydrator.Hydrate(
                        model.AccountTypeID, typeof(AccountType), maxDepth, nextDepth
                    );

                    if (model.CreatedByID != null)
                    {
                        entity.CreatedBy = (User)referenceHydrator.Hydrate(
                            model.CreatedByID, typeof(User), maxDepth, nextDepth
                        );
                    }
                }
            }
        }
    }
}