using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaEdges
{
    public class BacklogItemTypeSchemaEdgeHydrator : AbstractEntityHydrator
    {
        public BacklogItemTypeSchemaEdgeHydrator(DBContext _dbContext) : base(_dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchemaEdgeModel)
            ) && to == typeof(BacklogItemTypeSchemaEdge);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is BacklogItemTypeSchemaEdgeModel)
            {
                var model = (BacklogItemTypeSchemaEdgeModel)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemTypeSchemaEdge.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemTypeSchemaEdge), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new BacklogItemTypeSchemaEdge(
                        model.SchemaID, model.FromTypeID, model.ToTypeID);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.BacklogItemTypeSchemaEdge.Find(from);
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

            var entity = (BacklogItemTypeSchemaEdge)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemTypeSchemaEdgeModel)
            {
                var model = (BacklogItemTypeSchemaEdgeModel)from;

                entity.ID = model.ID;
                entity.SchemaID = model.SchemaID;
                entity.FromTypeID = model.FromTypeID;
                entity.ToTypeID = model.ToTypeID;
                entity.CreatedOn = model.CreatedOn;
                entity.CreatedByID = model.CreatedByID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.Schema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                        model.SchemaID, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );

                    entity.FromType = (BacklogItemType)referenceHydrator.Hydrate(
                        model.FromTypeID, typeof(BacklogItemType), maxDepth, nextDepth
                    );

                    entity.ToType = (BacklogItemType)referenceHydrator.Hydrate(
                        model.ToTypeID, typeof(BacklogItemType), maxDepth, nextDepth
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
