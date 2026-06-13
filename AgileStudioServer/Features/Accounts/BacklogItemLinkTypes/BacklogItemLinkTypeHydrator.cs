using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeHydrator : AbstractEntityHydrator
    {
        public BacklogItemLinkTypeHydrator(DBContext _dbContext) : base(_dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeModel)
            ) && to == typeof(BacklogItemLinkType);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is BacklogItemLinkTypeModel)
            {
                var model = (BacklogItemLinkTypeModel)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemLinkType.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemLinkType), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new BacklogItemLinkType(
                        model.Title, model.TitleOpposite, model.AccountID);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.BacklogItemLinkType.Find(from);
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

            var entity = (BacklogItemLinkType)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemLinkTypeModel)
            {
                var model = (BacklogItemLinkTypeModel)from;

                entity.ID = model.ID;
                entity.Title = model.Title;
                entity.TitleOpposite = model.TitleOpposite;
                entity.AccountID = model.AccountID;
                entity.Description = model.Description;
                entity.CreatedOn = model.CreatedOn;
                entity.CreatedByID = model.CreatedByID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.Account = (Account)referenceHydrator.Hydrate(
                        model.AccountID, typeof(Account), maxDepth, nextDepth
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
