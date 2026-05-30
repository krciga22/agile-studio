using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.AccountTypes
{
    public class AccountTypeDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(AccountTypeModel)
            ) && to == typeof(AccountTypeDto);
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

            AccountTypeModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (AccountTypeModel)referenceHydrator.Hydrate(
                    from, typeof(AccountTypeModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is AccountTypeModel)
            {
                model = (AccountTypeModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                dto = new AccountTypeDto(model.ID, model.Title, model.CreatedOn);
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

            var dto = (AccountTypeDto)to;
            int nextDepth = depth + 1;

            if (from is AccountTypeModel)
            {
                var model = (AccountTypeModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth && model.CreatedByID != null)
                {
                    dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                        model.CreatedByID, typeof(UserSummaryDto), maxDepth, depth
                    );
                }
            }
        }
    }
}