using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.BacklogItems
{
    public class BacklogItemService : AbstractModelService<BacklogItemModel, int>
    {
        private readonly BacklogItemRepository _BacklogItemRepository;
        private readonly ServiceContext _ServiceContext;

        public BacklogItemService(
            BacklogItemRepository backlogItemRepository,
            ServiceContext serviceContext)
        {
            _BacklogItemRepository = backlogItemRepository;
            _ServiceContext = serviceContext;
        }

        public virtual PaginationResults<BacklogItemModel> GetByProjectId(int projectId)
        {
            return _BacklogItemRepository.GetByProjectId(projectId, _ServiceContext);
        }

        public virtual List<BacklogItemModel> GetByProjectIdAndBacklogItemTypeId(int projectId, int backlogItemTypeId)
        {
            return _BacklogItemRepository.GetByProjectIdAndBacklogItemTypeId(projectId, backlogItemTypeId);
        }

        public virtual PaginationResults<BacklogItemModel> GetChildBacklogItems(int parentBacklogItemId)
        {
            return _BacklogItemRepository.GetChildBacklogItems(parentBacklogItemId, _ServiceContext);
        }

        public virtual BacklogItemModel? GetParentBacklogItem(int id)
        {
            return _BacklogItemRepository.GetParentBacklogItem(id);
        }

        public override PaginationResults<BacklogItemModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<BacklogItemModel> GetSubCollection(String parentResourceType, Object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.ProjectsProject:
                    return GetByProjectId(int.Parse(id[0].ToString()!));
                case ResourceTypes.BacklogItemsBacklogItem:
                    return GetChildBacklogItems(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        public override BacklogItemModel Get(int id)
        {
            return _BacklogItemRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(BacklogItemModel), id.ToString());
        }

        public override BacklogItemModel Create(BacklogItemModel backlogItem)
        {
            return _BacklogItemRepository.Create(backlogItem);
        }

        public override BacklogItemModel Update(BacklogItemModel backlogItem)
        {
            return _BacklogItemRepository.Update(backlogItem);
        }

        public override void Delete(BacklogItemModel backlogItem)
        {
            _BacklogItemRepository.Delete(backlogItem);
        }

        public override int GetIdentifier(BacklogItemModel backlogItem)
        {
            return backlogItem.ID;
        }
    }
}
