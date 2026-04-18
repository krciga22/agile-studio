using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaModelHydrator : AbstractModelHydrator
    {
        public BacklogItemTypeSchemaModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchema) ||
                from == typeof(BacklogItemTypeSchemaPostDto) ||
                from == typeof(BacklogItemTypeSchemaPatchDto)
            ) && to == typeof(BacklogItemTypeSchemaModel);
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
                var backlogItemTypeSchema = _DBContext.BacklogItemTypeSchema.Find(from);
                if (backlogItemTypeSchema != null)
                {
                    from = backlogItemTypeSchema;
                }
            }

            if (from is BacklogItemTypeSchema)
            {
                var entity = (BacklogItemTypeSchema)from;
                model = new BacklogItemTypeSchemaModel(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemTypeSchemaPostDto)
            {
                var dto = (BacklogItemTypeSchemaPostDto)from;
                model = new BacklogItemTypeSchemaModel(dto.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemTypeSchemaPatchDto)
            {
                var dto = (BacklogItemTypeSchemaPatchDto)from;
                var entity = _DBContext.BacklogItemTypeSchema.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItemTypeSchemaModel), maxDepth, depth, referenceHydrator);
                    Hydrate(dto, model, maxDepth, depth, referenceHydrator);
                }
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

            var model = (BacklogItemTypeSchemaModel)to;

            if (from is BacklogItemTypeSchema)
            {
                var entity = (BacklogItemTypeSchema)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedById = entity.CreatedByID;
            }
            else if (from is BacklogItemTypeSchemaPostDto)
            {
                var dto = (BacklogItemTypeSchemaPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
            else if (from is BacklogItemTypeSchemaPatchDto)
            {
                var dto = (BacklogItemTypeSchemaPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
