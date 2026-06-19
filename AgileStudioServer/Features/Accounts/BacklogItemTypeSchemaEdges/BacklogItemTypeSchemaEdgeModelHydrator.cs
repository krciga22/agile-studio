using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeModelHydrator : AbstractModelHydrator
    {
        public BacklogItemTypeSchemaEdgeModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchemaEdge)
            ) && to == typeof(BacklogItemTypeSchemaEdgeModel);
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
                var backlogItemTypeSchemaEdge = _DBContext.BacklogItemTypeSchemaEdge.Find(from);
                if (backlogItemTypeSchemaEdge != null)
                {
                    from = backlogItemTypeSchemaEdge;
                }
            }

            if (from is BacklogItemTypeSchemaEdge)
            {
                var entity = (BacklogItemTypeSchemaEdge)from;
                model = new BacklogItemTypeSchemaEdgeModel(
                    entity.SchemaID, entity.FromTypeID, entity.ToTypeID);
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

            var model = (BacklogItemTypeSchemaEdgeModel)to;

            if (from is BacklogItemTypeSchemaEdge)
            {
                var entity = (BacklogItemTypeSchemaEdge)from;

                model.ID = entity.ID;
                model.SchemaID = entity.SchemaID;
                model.FromTypeID = entity.FromTypeID;
                model.ToTypeID = entity.ToTypeID;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
        }
    }
}
