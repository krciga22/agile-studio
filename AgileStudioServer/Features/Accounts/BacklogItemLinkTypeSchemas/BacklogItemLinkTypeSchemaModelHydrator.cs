using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Data;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas
{
    public class BacklogItemLinkTypeSchemaModelHydrator : AbstractModelHydrator
    {
        public BacklogItemLinkTypeSchemaModelHydrator(DBContext dbContext) : base(dbContext)
        {

        }

        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeSchema) ||
                from == typeof(BacklogItemLinkTypeSchemaPostDto) ||
                from == typeof(BacklogItemLinkTypeSchemaPatchDto)
            ) && to == typeof(BacklogItemLinkTypeSchemaModel);
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
                var backlogItemLinkTypeSchema = _DBContext.BacklogItemLinkTypeSchema.Find(from);
                if (backlogItemLinkTypeSchema != null)
                {
                    from = backlogItemLinkTypeSchema;
                }
            }

            if (from is BacklogItemLinkTypeSchema)
            {
                var entity = (BacklogItemLinkTypeSchema)from;
                model = new BacklogItemLinkTypeSchemaModel(entity.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemLinkTypeSchemaPostDto)
            {
                var dto = (BacklogItemLinkTypeSchemaPostDto)from;
                model = new BacklogItemLinkTypeSchemaModel(dto.Title);
                Hydrate(from, model, maxDepth, depth, referenceHydrator);
            }
            else if (from is BacklogItemLinkTypeSchemaPatchDto)
            {
                var dto = (BacklogItemLinkTypeSchemaPatchDto)from;
                var entity = _DBContext.BacklogItemLinkTypeSchema.Find(dto.ID);
                if (entity != null)
                {
                    model = Hydrate(entity, typeof(BacklogItemLinkTypeSchemaModel), maxDepth, depth, referenceHydrator);
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

            var model = (BacklogItemLinkTypeSchemaModel)to;

            if (from is BacklogItemLinkTypeSchema)
            {
                var entity = (BacklogItemLinkTypeSchema)from;

                model.ID = entity.ID;
                model.Title = entity.Title;
                model.Description = entity.Description;
                model.CreatedOn = entity.CreatedOn;
                model.CreatedByID = entity.CreatedByID;
            }
            else if (from is BacklogItemLinkTypeSchemaPostDto)
            {
                var dto = (BacklogItemLinkTypeSchemaPostDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
            else if (from is BacklogItemLinkTypeSchemaPatchDto)
            {
                var dto = (BacklogItemLinkTypeSchemaPatchDto)from;
                model.Title = dto.Title;
                model.Description = dto.Description;
            }
        }
    }
}
