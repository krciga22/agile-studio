
using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Hydrator.Exceptions;
using AgileStudioServer.Core.Hydrators.Exceptions;
using AgileStudioServer.Features.Accounts.Accounts;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Users.Users;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowDtoHydrator : AbstractDtoHydrator
    {
        public override bool Supports(Type from, Type to)
        {
            return (
                from == typeof(int) ||
                from == typeof(WorkflowModel)
            ) && to == typeof(WorkflowDto);
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

            WorkflowModel? model = null;
            if (from is int && referenceHydrator != null)
            {
                model = (WorkflowModel)referenceHydrator.Hydrate(
                    from, typeof(WorkflowModel), maxDepth, depth, referenceHydrator
                );
            }
            else if (from is WorkflowModel)
            {
                model = (WorkflowModel)from;
            }

            object? dto = null;
            if (model != null && referenceHydrator != null)
            {
                var accountSummaryDto = (AccountSummaryDto)referenceHydrator.Hydrate(
                    model.AccountID, typeof(AccountSummaryDto), maxDepth, depth
                );

                dto = new WorkflowDto(model.ID, model.Title, model.CreatedOn, accountSummaryDto);
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

            var dto = (WorkflowDto)to;
            int nextDepth = depth + 1;

            if (from is WorkflowModel)
            {
                var model = (WorkflowModel)from;
                dto.ID = model.ID;
                dto.Title = model.Title;
                dto.Description = model.Description;
                dto.CreatedOn = model.CreatedOn;

                if (referenceHydrator != null && nextDepth <= maxDepth)
                {
                    dto.Account = (AccountSummaryDto)referenceHydrator.Hydrate(
                        model.AccountID, typeof(AccountSummaryDto), maxDepth, depth
                    );

                    if (model.CreatedById != null)
                    {
                        dto.CreatedBy = (UserSummaryDto)referenceHydrator.Hydrate(
                            model.CreatedById, typeof(UserSummaryDto), maxDepth, depth
                        );
                    }

                    if (model.DefaultWorkflowStateID != null)
                    {
                        dto.DefaultWorkflowState = (WorkflowStateSummaryDto)referenceHydrator.Hydrate(
                            model.DefaultWorkflowStateID, typeof(WorkflowStateSummaryDto), maxDepth, depth
                        );
                    }
                }
            }
        }
    }
}
