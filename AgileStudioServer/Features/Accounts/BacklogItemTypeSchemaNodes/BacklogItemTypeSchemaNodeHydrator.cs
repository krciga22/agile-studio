using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Repositories.Exceptions;
using AgileStudioServer.Data;
using AgileStudioServer.Features.Accounts.BacklogItemTypes;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemTypeSchemaNodes
{
    public class BacklogItemTypeSchemaNodeHydrator : AbstractEntityHydrator
    {
        public BacklogItemTypeSchemaNodeHydrator(DBContext _dbContext) : base(_dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchemaNodeModel)
            ) && to == typeof(BacklogItemTypeSchemaNode);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            object? entity = null;

            if (from is BacklogItemTypeSchemaNodeModel)
            {
                var model = (BacklogItemTypeSchemaNodeModel)from;
                if (model.ID > 0)
                {
                    entity = _DBContext.BacklogItemTypeSchemaNode.Find(model.ID);
                    if (entity == null)
                    {
                        throw new EntityNotFoundException(
                            nameof(BacklogItemTypeSchemaNode), model.ID.ToString());
                    }
                }
                else
                {
                    entity = new BacklogItemTypeSchemaNode(
                        model.SchemaID, model.BacklogItemTypeID);
                }

                if (entity != null)
                {
                    Hydrate(model, entity, maxDepth, depth, referenceHydrator);
                }
            }
            else if (from is int)
            {
                entity = _DBContext.BacklogItemTypeSchemaNode.Find(from);
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

            var entity = (BacklogItemTypeSchemaNode)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemTypeSchemaNodeModel)
            {
                var model = (BacklogItemTypeSchemaNodeModel)from;

                entity.ID = model.ID;
                entity.SchemaID = model.SchemaID;
                entity.CreatedOn = model.CreatedOn;
                entity.BacklogItemTypeID = model.BacklogItemTypeID;
                entity.CreatedByID = model.CreatedByID;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    entity.Schema = (BacklogItemTypeSchema)referenceHydrator.Hydrate(
                        model.SchemaID, typeof(BacklogItemTypeSchema), maxDepth, nextDepth
                    );

                    entity.BacklogItemType = (BacklogItemType)referenceHydrator.Hydrate(
                        model.BacklogItemTypeID, typeof(BacklogItemType), maxDepth, nextDepth
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
