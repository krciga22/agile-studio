using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.CoreFeatures.Users.Users;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryHydrator : AbstractEntityHydrator
    {
        public BacklogItemLinkTypeSchemaEntryHydrator(DBContext _dbContext) : base(_dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeSchemaEntryModel)
            ) && to == typeof(BacklogItemLinkTypeSchemaEntry);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is BacklogItemLinkTypeSchemaEntryModel)
            {
                var model = (BacklogItemLinkTypeSchemaEntryModel)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemLinkTypeSchemaEntry.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemLinkTypeSchemaEntry), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new BacklogItemLinkTypeSchemaEntry(
                        model.BacklogItemLinkTypeSchemaID, 
                        model.BacklogItemLinkTypeID);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.BacklogItemLinkTypeSchemaEntry.Find(from);
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

            var entity = (BacklogItemLinkTypeSchemaEntry)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemLinkTypeSchemaEntryModel)
            {
                var model = (BacklogItemLinkTypeSchemaEntryModel)from;

                entity.ID = model.ID;
                entity.BacklogItemLinkTypeSchemaID = model.BacklogItemLinkTypeSchemaID;
                entity.BacklogItemLinkTypeID = model.BacklogItemLinkTypeID;
                entity.CreatedOn = model.CreatedOn;
                entity.CreatedByID = model.CreatedByID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.BacklogItemLinkTypeSchema = (BacklogItemLinkTypeSchema)referenceHydrator.Hydrate(
                        model.BacklogItemLinkTypeSchemaID, typeof(BacklogItemLinkTypeSchema), maxDepth, nextDepth
                    );

                    entity.BacklogItemLinkType = (BacklogItemLinkType)referenceHydrator.Hydrate(
                        model.BacklogItemLinkTypeID, typeof(BacklogItemLinkType), maxDepth, nextDepth
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
