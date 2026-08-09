using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using AgileStudioServer.Core.Services.Exceptions;
using AgileStudioServer.Features.Resources.Resource;

namespace AgileStudioServer.Features.Projects.BacklogItemStatuses
{
    public class BacklogItemStatusService : AbstractModelService<BacklogItemStatusModel, int>
    {
        private readonly BacklogItemStatusRepository _BacklogItemStatusRepository;
        private readonly ServiceContext _ServiceContext;

        public BacklogItemStatusService(
            BacklogItemStatusRepository backlogItemStatusRepository,
            ServiceContext serviceContext)
        {
            _BacklogItemStatusRepository = backlogItemStatusRepository;
            _ServiceContext = serviceContext;
        }

        public virtual PaginationResults<BacklogItemStatusModel> GetByBacklogItemId(int backlogItemId)
        {
            return _BacklogItemStatusRepository.GetByBacklogItemId(backlogItemId);
        }

        public override PaginationResults<BacklogItemStatusModel> GetCollection()
        {
            throw new NotImplementedException();
        }

        public override PaginationResults<BacklogItemStatusModel> GetSubCollection(string parentResourceType, object[] id)
        {
            switch (parentResourceType)
            {
                case ResourceTypes.BacklogItemsBacklogItem:
                    return GetByBacklogItemId(int.Parse(id[0].ToString()!));
                default:
                    throw new ArgumentException($"Unsupported parent resource type: {parentResourceType}");
            }
        }

        /// <exception cref="ModelNotFoundException"></exception>
        public override BacklogItemStatusModel Get(int id)
        {
            return _BacklogItemStatusRepository.Get(id) ??
                throw new ModelNotFoundException(nameof(BacklogItemStatusModel), id.ToString());
        }

        public override int GetIdentifier(BacklogItemStatusModel backlogItemStatus)
        {
            return backlogItemStatus.ID;
        }

        public override BacklogItemStatusModel Create(BacklogItemStatusModel backlogItemStatus)
        {
            return _BacklogItemStatusRepository.Create(backlogItemStatus);
        }

        public override BacklogItemStatusModel Update(BacklogItemStatusModel backlogItemStatus)
        {
            return _BacklogItemStatusRepository.Update(backlogItemStatus);
        }

        public override void Delete(BacklogItemStatusModel backlogItemStatus)
        {
            _BacklogItemStatusRepository.Delete(backlogItemStatus);
        }
    }
}