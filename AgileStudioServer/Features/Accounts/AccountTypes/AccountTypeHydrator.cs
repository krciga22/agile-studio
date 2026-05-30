using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.AccountTypes
{
    public class AccountTypeHydrator(DBContext _dbContext) : AbstractEntityHydrator(_dbContext)
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(AccountTypeModel)
            ) && to == typeof(AccountType);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is AccountTypeModel)
            {
                var model = (AccountTypeModel)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.AccountType.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(nameof(AccountType), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new AccountType(model.Title);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.AccountType.Find(from);
            }

            if (entity == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return entity;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var entity = (AccountType)to;
            int nextDepth = depth + 1;

            if (from is AccountTypeModel)
            {
                var model = (AccountTypeModel)from;

                entity.ID = model.ID;
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.CreatedOn = model.CreatedOn;
                entity.CreatedByID = model.CreatedByID;

                if (referenceHydrator != null && nextDepth <= maxDepth && model.CreatedByID != null)
                {
                    entity.CreatedBy = (User)referenceHydrator.Hydrate(
                        model.CreatedByID, typeof(User), maxDepth, nextDepth
                    );
                }
            }
        }
    }
}