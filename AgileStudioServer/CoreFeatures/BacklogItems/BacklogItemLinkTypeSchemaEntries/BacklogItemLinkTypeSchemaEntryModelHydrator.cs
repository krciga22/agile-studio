using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryModelHydrator : AbstractModelHydrator
    {
        public BacklogItemLinkTypeSchemaEntryModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeSchemaEntry) ||
                from == typeof(BacklogItemLinkTypeSchemaEntryPostDto)
            ) && to == typeof(BacklogItemLinkTypeSchemaEntryModel);
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
                var backlogItemLinkTypeSchema = _DBContext.BacklogItemLinkTypeSchemaEntry.Find(from);
                if (backlogItemLinkTypeSchema != null)
                {
                    from = backlogItemLinkTypeSchema;
                }
            }

            if (from is BacklogItemLinkTypeSchemaEntry)
            {
                var entity = (BacklogItemLinkTypeSchemaEntry)from;
                model = new BacklogItemLinkTypeSchemaEntryModel(
                    entity.BacklogItemLinkTypeSchemaID,
                    entity.BacklogItemLinkTypeID);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemLinkTypeSchemaEntryPostDto)
            {
                var dto = (BacklogItemLinkTypeSchemaEntryPostDto)from;
                model = new BacklogItemLinkTypeSchemaEntryModel(
                    dto.BacklogItemLinkTypeSchemaID,
                    dto.BacklogItemLinkTypeID);
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

            var model = (BacklogItemLinkTypeSchemaEntryModel)to;

            if (from is BacklogItemLinkTypeSchemaEntry)
            {
                var entity = (BacklogItemLinkTypeSchemaEntry)from;

                model.ID = entity.ID;
                model.BacklogItemLinkTypeSchemaID = entity.BacklogItemLinkTypeSchemaID;
                model.BacklogItemLinkTypeID = entity.BacklogItemLinkTypeID;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
            else if (from is BacklogItemLinkTypeSchemaEntryPostDto)
            {
                var dto = (BacklogItemLinkTypeSchemaEntryPostDto)from;
                model.BacklogItemLinkTypeSchemaID = dto.BacklogItemLinkTypeSchemaID;
                model.BacklogItemLinkTypeID = dto.BacklogItemLinkTypeID;
            }
        }
    }
}
