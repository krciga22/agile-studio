using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEntries
{
    public class BacklogItemTypeSchemaEntryModelHydrator : AbstractModelHydrator
    {
        public BacklogItemTypeSchemaEntryModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchemaEntry)
            ) && to == typeof(BacklogItemTypeSchemaEntryModel);
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
                var backlogItemTypeSchemaEntry = _DBContext.BacklogItemTypeSchemaEntry.Find(from);
                if (backlogItemTypeSchemaEntry != null)
                {
                    from = backlogItemTypeSchemaEntry;
                }
            }

            if (from is BacklogItemTypeSchemaEntry)
            {
                var entity = (BacklogItemTypeSchemaEntry)from;
                model = new BacklogItemTypeSchemaEntryModel(
                    entity.ChildTypeID, entity.ParentTypeID, entity.SchemaID);
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

            var model = (BacklogItemTypeSchemaEntryModel)to;

            if (from is BacklogItemTypeSchemaEntry)
            {
                var entity = (BacklogItemTypeSchemaEntry)from;

                model.ID = entity.ID;
                model.CreatedOn = entity.CreatedOn;
                model.ChildTypeID = entity.ChildTypeID;
                model.ParentTypeID = entity.ParentTypeID;
                model.SchemaID = entity.SchemaID;
                model.CreatedByID = entity.CreatedByID;
            }
        }
    }
}
