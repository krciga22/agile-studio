using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Features.Accounts.AccountTypes;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.Accounts
{
    public class AccountDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(AccountModel)
            ) && to == typeof(AccountDto);
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

            AccountModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (AccountModel)referenceHydrator.Hydrate(
                    from, typeof(AccountModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is AccountModel)
            {
                model = (AccountModel)from;
            }

            object? dto = null;
            if (model != null)
            {
                var accountTypeDto = (AccountTypeDto)referenceHydrator.Hydrate(
                    model.AccountTypeID, typeof(AccountTypeDto), maxDepth, depth
                );

                dto = new AccountDto(model.ID, accountTypeDto, model.CreatedOn);
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

            var dto = (AccountDto)to;
            int nextDepth = depth + 1;

            if (from is AccountModel)
            {
                var model = (AccountModel)from;

                dto.ID = model.ID;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.AccountType = (AccountTypeDto)referenceHydrator.Hydrate(
                        model.AccountTypeID, typeof(AccountTypeDto), maxDepth, nextDepth
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