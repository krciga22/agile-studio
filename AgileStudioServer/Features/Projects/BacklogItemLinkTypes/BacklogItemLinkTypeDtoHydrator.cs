
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;

namespace AgileStudioServer.Features.Projects.BacklogItemLinkTypes
{
    public class BacklogItemLinkTypeDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeModel)
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
            if (model != null && referenceHydrator != null)
            {
                AccountSummaryDto accountSummaryDto = (AccountSummaryDto) referenceHydrator.Hydrate(
                    model.AccountID, typeof(AccountSummaryDto));

                dto = new BacklogItemLinkTypeDto(
                    model.ID, model.Title, model.TitleOpposite, accountSummaryDto);

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

            if (from is BacklogItemLinkTypeModel)
            {
                var model = (BacklogItemLinkTypeModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.TitleOpposite = model.TitleOpposite;
                dto.Description = model.Description;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.Account = (AccountSummaryDto)referenceHydrator.Hydrate(
                        model.AccountID, typeof(AccountSummaryDto), maxDepth, nextDepth
                    );
                }
            }
        }
    }
}
