
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeSummaryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeModel)
            ) && to == typeof(BacklogItemLinkTypeSummaryDto);
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

            BacklogItemLinkTypeModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemLinkTypeModel)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemLinkTypeModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemLinkTypeModel)
            {
                model = (BacklogItemLinkTypeModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new BacklogItemLinkTypeSummaryDto(model.ID, model.Title, model.TitleOpposite);
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

            var dto = (BacklogItemLinkTypeSummaryDto)to;

            if (from is BacklogItemLinkTypeModel)
            {
                var model = (BacklogItemLinkTypeModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.TitleOpposite = model.TitleOpposite;
                dto.Description = model.Description;
            }
        }
    }
}
