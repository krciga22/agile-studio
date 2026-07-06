using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.BacklogItemTypeSchemas;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Projects.BacklogItemTypeSchemas
{
    public class BacklogItemTypeSchemaForProjectDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(BacklogItemTypeSchemaModel)
            ) && to == typeof(BacklogItemTypeSchemaForProjectDto);
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
                var accountSummaryDto = (AccountSummaryDto)referenceHydrator.Hydrate(
                    model.AccountID, typeof(AccountSummaryDto), maxDepth, depth
                );

                dto = new BacklogItemTypeSchemaForProjectDto(model.ID, model.Title);
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

            var dto = (BacklogItemTypeSchemaForProjectDto)to;
            int nextDepth = depth + 1;

            if (from is BacklogItemTypeSchemaModel)
            {
                var model = (BacklogItemTypeSchemaModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
            }
        }
    }
}
