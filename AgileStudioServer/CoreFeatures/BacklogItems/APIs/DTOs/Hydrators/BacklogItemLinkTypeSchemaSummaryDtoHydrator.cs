
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;

namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs.Hydrators
{
    public class BacklogItemLinkTypeSchemaSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeSchema)
            ) && to == typeof(BacklogItemLinkTypeSchemaSummaryDto);
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

            BacklogItemLinkTypeSchema? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemLinkTypeSchema)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemLinkTypeSchema), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemLinkTypeSchema)
            {
                model = (BacklogItemLinkTypeSchema)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new BacklogItemLinkTypeSchemaSummaryDto(model.ID, model.Title);
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

            var dto = (BacklogItemLinkTypeSchemaSummaryDto)to;

            if (from is BacklogItemLinkTypeSchema)
            {
                var model = (BacklogItemLinkTypeSchema)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
            }
        }
    }
}
