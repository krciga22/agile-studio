using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Features.Projects.BacklogItems
{
    public class BacklogItemService : AbstractService
    {
        private readonly BacklogItemRepository _BacklogItemRepository;

        public BacklogItemService(BacklogItemRepository backlogItemRepository)
        {
            _BacklogItemRepository = backlogItemRepository;
        }

        public virtual List<BacklogItemModel> GetByProjectId(int projectId)
        {
            return _BacklogItemRepository.GetByProjectId(projectId);
        }

        public virtual List<BacklogItemModel> GetByProjectIdAndBacklogItemTypeId(int projectId, int backlogItemTypeId)
        {
            return _BacklogItemRepository.GetByProjectIdAndBacklogItemTypeId(projectId, backlogItemTypeId);
        }

        public virtual PaginationResults<BacklogItemModel> GetChildBacklogItems(int parentBacklogItemId, PaginationDetails? paginationDetails = null)
        {
            return _BacklogItemRepository.GetChildBacklogItems(parentBacklogItemId, paginationDetails);
        }

        public virtual BacklogItemModel? GetParentBacklogItem(int id)
        {
            return _BacklogItemRepository.GetParentBacklogItem(id);
        }

        public virtual BacklogItemModel? Get(int id)
        {
            return _BacklogItemRepository.Get(id);
        }

        public virtual BacklogItemModel Create(BacklogItemModel backlogItem)
        {
            return _BacklogItemRepository.Create(backlogItem);
        }

        public virtual BacklogItemModel Update(BacklogItemModel backlogItem)
        {
            return _BacklogItemRepository.Update(backlogItem);
        }

        public virtual void Delete(BacklogItemModel backlogItem)
        {
            _BacklogItemRepository.Delete(backlogItem);
        }
    }
}
