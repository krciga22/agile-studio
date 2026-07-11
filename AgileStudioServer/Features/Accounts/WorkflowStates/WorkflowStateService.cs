using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.WorkflowStates
{
    public class WorkflowStateService : AbstractModelService<WorkflowStateModel, int>
    {
        private WorkflowStateRepository _WorkflowStateRepository;

        private ServiceContext _ServiceContext;

        public WorkflowStateService(
            WorkflowStateRepository workflowStateRepository, 
            ServiceContext serviceContext)
        {
            _WorkflowStateRepository = workflowStateRepository;
            _ServiceContext = serviceContext;
        }

        public virtual PaginationResults<WorkflowStateModel> GetByWorkflowId(int workflowId)
        {
            return _WorkflowStateRepository.GetByWorkflowId(workflowId, _ServiceContext);
        }

        public override PaginationResults<WorkflowStateModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<WorkflowStateModel> GetSubCollection(string parentResourceType, object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.AccountsWorkflow:
                    return GetByWorkflowId(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public override WorkflowStateModel Get(int id)
        {
            return _WorkflowStateRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(WorkflowStateModel), id.ToString());
        }

        public override int GetIdentifier(WorkflowStateModel model)
        {
            return model.ID;
        }

        public override WorkflowStateModel Create(WorkflowStateModel workflowState)
        {
            return _WorkflowStateRepository.Create(workflowState);
        }

        public override WorkflowStateModel Update(WorkflowStateModel workflowState)
        {
            return _WorkflowStateRepository.Update(workflowState);
        }

        public override void Delete(WorkflowStateModel workflowState)
        {
            _WorkflowStateRepository.Delete(workflowState);
        }
    }
}
