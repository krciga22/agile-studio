using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Accounts.Workflows
{
    public class WorkflowService : AbstractModelService<WorkflowModel, int>
    {
        private readonly WorkflowRepository _WorkflowRepository;

        private readonly ServiceContext _ServiceContext;

        public WorkflowService(
            WorkflowRepository workflowRepository, 
            ServiceContext serviceContext)
        {
            _WorkflowRepository = workflowRepository;
            _ServiceContext = serviceContext;
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
            return _WorkflowRepository.Create(workflow);
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
