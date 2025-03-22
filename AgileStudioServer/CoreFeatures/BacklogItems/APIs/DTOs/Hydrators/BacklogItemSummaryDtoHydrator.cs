
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;

namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs.Hydrators
{
    public class BacklogItemSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItem)
            ) && to == typeof(BacklogItemSummaryDto);
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

            BacklogItem? model = null;
            if (from is int)
            {
                model = (BacklogItem)referenceHydrator.Hydrate(
                    from, typeof(BacklogItem), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItem)
            {
                model = (BacklogItem)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new BacklogItemSummaryDto(model.ID, model.Title);
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

            var dto = (BacklogItemSummaryDto)to;

            if (from is BacklogItem)
            {
                var model = (BacklogItem)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
            }
        }
    }
}
