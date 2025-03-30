
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;

namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs.Hydrators
{
    public class BacklogItemTypeSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeModel)
            ) && to == typeof(BacklogItemTypeSummaryDto);
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

            BacklogItemTypeModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemTypeModel)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemTypeModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemTypeModel)
            {
                model = (BacklogItemTypeModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new BacklogItemTypeSummaryDto(model.ID, model.Title, model.CreatedOn);
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

            var dto = (BacklogItemTypeSummaryDto)to;

            if (from is BacklogItemTypeModel)
            {
                var model = (BacklogItemTypeModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;
            }
        }
    }
}
