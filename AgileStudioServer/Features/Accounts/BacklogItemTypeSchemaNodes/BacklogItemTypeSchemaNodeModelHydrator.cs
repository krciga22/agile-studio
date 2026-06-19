using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeModelHydrator : AbstractModelHydrator
    {
        public BacklogItemTypeSchemaNodeModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchemaNode)
            ) && to == typeof(BacklogItemTypeSchemaNodeModel);
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
                var backlogItemTypeSchemaNode = _DBContext.BacklogItemTypeSchemaNode.Find(from);
                if (backlogItemTypeSchemaNode != null)
                {
                    from = backlogItemTypeSchemaNode;
                }
            }

            if (from is BacklogItemTypeSchemaNode)
            {
                var entity = (BacklogItemTypeSchemaNode)from;
                model = new BacklogItemTypeSchemaNodeModel(
                    entity.SchemaID, entity.BacklogItemTypeID);
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

            var model = (BacklogItemTypeSchemaNodeModel)to;

            if (from is BacklogItemTypeSchemaNode)
            {
                var entity = (BacklogItemTypeSchemaNode)from;

                model.ID = entity.ID;
                model.SchemaID = entity.SchemaID;
                model.BacklogItemTypeID = entity.BacklogItemTypeID;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
        }
    }
}
