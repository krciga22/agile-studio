
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Users.Users;

namespace AgileStudioServer.CoreFeatures.Accounts.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchemaModel)
            ) && to == typeof(BacklogItemTypeSchemaDto);
        }

        public override object Hydrate(object from, Type to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to))
            {
                throw new HydrationNotSupportedException(from.GetType(), to);
            }

            if (referenceHydrator == null)
            {
                throw new ReferenceHydratorRequiredException(this);
            }

            BacklogItemTypeSchemaModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemTypeSchemaModel)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemTypeSchemaModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemTypeSchemaModel)
            {
                model = (BacklogItemTypeSchemaModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new BacklogItemTypeSchemaDto(model.ID, model.Title, model.CreatedOn);
                Hydrate(model, dto, maxDepth, depth, referenceHydrator);
            }

            if (dto == null)
            {
                throw new HydrationFailedException(from.GetType(), to);
            }

            return dto;
        }

        public override void Hydrate(object from, object to, int maxDepth, int depth, IHydrator? referenceHydrator = null)
        {
            if (!Supports(from.GetType(), to.GetType()))
            {
                throw new HydrationNotSupportedException(from.GetType(), to.GetType());
            }

            var dto = (BacklogItemTypeSchemaDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemTypeSchemaModel)
            {
                var model = (BacklogItemTypeSchemaModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    if (model.CreatedById != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedById, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
