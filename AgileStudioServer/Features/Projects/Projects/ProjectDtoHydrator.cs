
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Features.Accounts.BacklogItemLinkTypeSchemas;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;
using AgileStudioServer.Features.Accounts.Accounts;

namespace AgileStudioServer.Features.Projects.Projects
{
    public class ProjectDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(ProjectModel)
            ) && to == typeof(ProjectDto);
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

            ProjectModel? model = null;
            if (from is int)
            {
                model = (ProjectModel)referenceHydrator.Hydrate(
                    from, typeof(ProjectModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is ProjectModel)
            {
                model = (ProjectModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                var accountSummaryDto = (AccountSummaryDto)referenceHydrator.Hydrate(
                    model.AccountID, typeof(AccountSummaryDto), maxDepth, depth
                );

                var backlogItemTypeSchemaSummaryDto = (BacklogItemTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemTypeSchemaID, typeof(BacklogItemTypeSchemaSummaryDto), maxDepth, depth
                );

                var backlogItemLinkTypeSchemaSummaryDto = (BacklogItemLinkTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                    model.BacklogItemLinkTypeSchemaID, typeof(BacklogItemLinkTypeSchemaSummaryDto), maxDepth, depth
                );

                dto = new ProjectDto(
                    model.ID,
                    accountSummaryDto,
                    model.Title, 
                    model.CreatedOn, 
                    backlogItemTypeSchemaSummaryDto, 
                    backlogItemLinkTypeSchemaSummaryDto);
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

            var dto = (ProjectDto)to;
            int nextDepth = depth + 1;

            if (from is ProjectModel)
            {
                var model = (ProjectModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.Account = (AccountSummaryDto)referenceHydrator.Hydrate(
                        model.AccountID, typeof(AccountSummaryDto), maxDepth, depth
                    );

                    dto.BacklogItemTypeSchema = (BacklogItemTypeSchemaSummaryDto)referenceHydrator.Hydrate(
                        model.BacklogItemTypeSchemaID, typeof(BacklogItemTypeSchemaSummaryDto), maxDepth, depth
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
