
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.CoreFeatures.Users.APIs.DTOs;
using AgileStudioServer.CoreFeatures.BacklogItems.Services.Models;

namespace AgileStudioServer.CoreFeatures.BacklogItems.APIs.DTOs.Hydrators
{
    public class BacklogItemLinkTypeDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkType)
            ) && to == typeof(BacklogItemLinkTypeDto);
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

            BacklogItemLinkType? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemLinkType)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemLinkType), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemLinkType)
            {
                model = (BacklogItemLinkType)from;
            }

            object? dto = null;
            if (model != null && referenceHydrator != null)
            {
                dto = new BacklogItemLinkTypeDto(
                    model.ID, model.Title, model.TitleOpposite, model.CreatedOn);

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

            var dto = (BacklogItemLinkTypeDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemLinkType)
            {
                var model = (BacklogItemLinkType)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.TitleOpposite = model.TitleOpposite;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    if (model.CreatedByID != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedByID, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
