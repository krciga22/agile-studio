using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaHydrator : AbstractEntityHydrator
    {
        public BacklogItemLinkTypeSchemaHydrator(DBContext _dbContext) : base(_dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeSchemaModel)
            ) && to == typeof(BacklogItemLinkTypeSchema);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is BacklogItemLinkTypeSchemaModel)
            {
                var model = (BacklogItemLinkTypeSchemaModel)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemLinkTypeSchema.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemLinkTypeSchema), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new BacklogItemLinkTypeSchema(model.Title, model.AccountID);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.BacklogItemLinkTypeSchema.Find(from);
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

            var entity = (BacklogItemLinkTypeSchema)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemLinkTypeSchemaModel)
            {
                var model = (BacklogItemLinkTypeSchemaModel)from;

                entity.ID = model.ID;
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.CreatedOn = model.CreatedOn;
                entity.CreatedByID = model.CreatedByID;
                entity.AccountID = model.AccountID;

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
