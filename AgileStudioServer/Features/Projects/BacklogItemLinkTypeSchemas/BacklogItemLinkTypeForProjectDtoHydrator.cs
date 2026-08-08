
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Projects.BacklogItemLinkTypeSchemas;

namespace AgileStudioServer.Features.Projects.BacklogItemLinkTypeSchemaSchemas
{
    public class BacklogItemLinkTypeSchemaForProjectDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeSchemaModel)
            ) && to == typeof(BacklogItemLinkTypeSchemaForProjectDto);
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

            BacklogItemLinkTypeSchemaModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemLinkTypeSchemaModel)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemLinkTypeSchemaModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemLinkTypeSchemaModel)
            {
                model = (BacklogItemLinkTypeSchemaModel)from;
            }

            object? dto = null;
            if (model != null && referenceHydrator != null)
            {
                AccountSummaryDto accountSummaryDto = (AccountSummaryDto) referenceHydrator.Hydrate(
                    model.AccountID, typeof(AccountSummaryDto));

                dto = new BacklogItemLinkTypeSchemaForProjectDto(
                    model.ID, model.Title, accountSummaryDto);

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

            var dto = (BacklogItemLinkTypeSchemaForProjectDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemLinkTypeSchemaModel)
            {
                var model = (BacklogItemLinkTypeSchemaModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
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
