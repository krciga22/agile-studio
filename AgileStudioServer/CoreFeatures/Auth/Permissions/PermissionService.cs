using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Auth.Permissions
{
    public class PermissionService(PermissionRepository permissionRepository) : AbstractService
    {
        private readonly PermissionRepository _PermissionRepository = permissionRepository;

        public virtual PaginationResults<PermissionModel> GetAll(ServiceContext serviceContext)
        {
            return _PermissionRepository.GetAll(serviceContext);
        }

        public virtual PermissionModel? Get(int id)
        {
            return _PermissionRepository.Get(id);
        }

        public virtual PermissionModel? GetByUUID(string uuid)
        {
            return _PermissionRepository.GetByUUID(uuid);
        }

        public virtual PermissionModel Create(PermissionModel permission)
        {
            return _PermissionRepository.Create(permission);
        }

        public virtual PermissionModel Update(PermissionModel permission)
        {
            return _PermissionRepository.Update(permission);
        }

        public virtual void Delete(PermissionModel permission)
        {
            _PermissionRepository.Delete(permission);
        }
    }
}