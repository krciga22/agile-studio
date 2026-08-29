using AgileStudioServer.Core.Data;
using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Accounts.WorkflowStates;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowService : AbstractModelService<WorkflowModel, int>
    {
        private readonly WorkflowRepository _WorkflowRepository;

        private readonly WorkflowStateRepository _WorkflowStateRepository;

        private readonly ServiceContext _ServiceContext;

        private TransactionService _TransactionService;

        public WorkflowService(
            WorkflowRepository workflowRepository,
            WorkflowStateRepository workflowStateRepository,
            ServiceContext serviceContext,
            TransactionService transactionService)
        {
            _WorkflowRepository = workflowRepository;
            _WorkflowStateRepository = workflowStateRepository;
            _ServiceContext = serviceContext;
            _TransactionService = transactionService;
        }

        public virtual PaginationResults<WorkflowModel> GetByAccountID(int accountID)
        {
            return _WorkflowRepository.GetByAccountID(accountID, _ServiceContext);
        }

        public override PaginationResults<WorkflowModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<WorkflowModel> GetSubCollection(string parentResourceType, object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.AccountsAccount:
                    return GetByAccountID(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public override WorkflowModel Get(int id)
        {
            return _WorkflowRepository.Get(id) ?? 
                throw new ModelNotFoundException(nameof(WorkflowModel), id.ToString());
        }

        public override int GetIdentifier(WorkflowModel model)
        {
            return model.ID;
        }

        public override WorkflowModel Create(WorkflowModel workflow)
        {
            return _TransactionService.ExecuteInTransaction<WorkflowModel>(() => {
                workflow = _WorkflowRepository.Create(workflow);

                String workflowStateTitle = WorkflowConstants.DefaultWorkflowStateTitle;
                WorkflowStateModel workflowStateModel = new(workflowStateTitle, workflow.ID);
                workflowStateModel = _WorkflowStateRepository.Create(workflowStateModel);

                workflow.DefaultWorkflowStateID = workflowStateModel.ID;
                _WorkflowRepository.StageUpdate(workflow);

                return workflow;
            });
        }

        public override WorkflowModel Update(WorkflowModel workflow)
        {
            return _WorkflowRepository.Update(workflow);
        }

        public override void Delete(WorkflowModel workflow)
        {
            _WorkflowRepository.Delete(workflow);
        }
    }
}
