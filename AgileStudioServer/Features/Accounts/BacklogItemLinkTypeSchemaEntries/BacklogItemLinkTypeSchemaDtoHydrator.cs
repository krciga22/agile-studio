
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypes;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemaEntries
{
    public class BacklogItemLinkTypeSchemaEntryDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemLinkTypeSchemaEntryModel)
            ) && to == typeof(BacklogItemLinkTypeSchemaEntryDto);
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

            BacklogItemLinkTypeSchemaEntryModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (BacklogItemLinkTypeSchemaEntryModel)referenceHydrator.Hydrate(
                    from, typeof(BacklogItemLinkTypeSchemaEntryModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is BacklogItemLinkTypeSchemaEntryModel)
            {
                model = (BacklogItemLinkTypeSchemaEntryModel)from;
            }

            object? dto = null;
            if (model != null && referenceHydrator != null)
            {
                BacklogItemLinkTypeSchemaSummaryDto backlogItemLinkTypeSchemaSummaryDto = (BacklogItemLinkTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemLinkTypeSchemaID, typeof(BacklogItemLinkTypeSchemaSummaryDto), maxDepth, depth, referenceHydrator
                );

                BacklogItemLinkTypeSummaryDto backlogItemLinkTypeSummaryDto = (BacklogItemLinkTypeSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemLinkTypeID, typeof(BacklogItemLinkTypeSummaryDto), maxDepth, depth, referenceHydrator
                );

                dto = new BacklogItemLinkTypeSchemaEntryDto(
                    model.ID,
                    backlogItemLinkTypeSchemaSummaryDto,
                    backlogItemLinkTypeSummaryDto,
                    model.CreatedOn);

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

            var dto = (BacklogItemLinkTypeSchemaEntryDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemLinkTypeSchemaEntryModel)
            {
                var model = (BacklogItemLinkTypeSchemaEntryModel)from;
                dto.ID = model.ID;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.BacklogItemLinkTypeSchemaSummaryDto = (BacklogItemLinkTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                        model.BacklogItemLinkTypeSchemaID, typeof(BacklogItemLinkTypeSchemaSummaryDto), maxDepth, depth
                    );

                    dto.BacklogItemLinkTypeSummaryDto = (BacklogItemLinkTypeSummaryDto)referenceHydrator.Hydrate(
                        model.BacklogItemLinkTypeID, typeof(BacklogItemLinkTypeSummaryDto), maxDepth, depth
                    );

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
