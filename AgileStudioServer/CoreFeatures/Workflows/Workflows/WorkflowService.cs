
namespace AgileStudioServer.CoreFeatures.Workflows.Workflows
{
    public class WorkflowService
    {
        private readonly WorkflowRepository _WorkflowRepository;

        public WorkflowService(WorkflowRepository workflowRepository)
        {
            _WorkflowRepository = workflowRepository;
        }

        public virtual List<WorkflowModel> GetAll()
        {
            return _WorkflowRepository.GetAll();
        }

        public virtual WorkflowModel? Get(int id)
        {
            return _WorkflowRepository.Get(id);
        }

        public virtual WorkflowModel Create(WorkflowModel workflow)
        {
            return _WorkflowRepository.Create(workflow);
        }

        public virtual WorkflowModel Update(WorkflowModel workflow)
        {
            return _WorkflowRepository.Update(workflow);
        }

        public virtual void Delete(WorkflowModel workflow)
        {
            _WorkflowRepository.Delete(workflow);
        }
    }
}
