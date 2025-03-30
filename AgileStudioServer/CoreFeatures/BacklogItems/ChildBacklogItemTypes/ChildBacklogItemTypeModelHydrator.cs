using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.BacklogItems.ChildBacklogItemTypes
{
    public class ChildBacklogItemTypeModelHydrator : AbstractModelHydrator
    {
        public ChildBacklogItemTypeModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(ChildBacklogItemType)
            ) && to == typeof(ChildBacklogItemTypeModel);
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
                var childBacklogItemType = _DBContext.ChildBacklogItemType.Find(from);
                if (childBacklogItemType != null)
                {
                    from = childBacklogItemType;
                }
            }

            if (from is ChildBacklogItemType)
            {
                var entity = (ChildBacklogItemType)from;
                model = new ChildBacklogItemTypeModel(
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

            var model = (ChildBacklogItemTypeModel)to;

            if (from is ChildBacklogItemType)
            {
                var entity = (ChildBacklogItemType)from;

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
