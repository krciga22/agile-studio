using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;
using System.Security;

namespace AgileStudioServer.CoreFeatures.Auth.Permissions
{
    public class PermissionService(PermissionRepository permissionRepository) : AbstractService
    {
        private readonly PermissionRepository _PermissionRepository = permissionRepository;

        public virtual PaginationResults<PermissionModel> GetAll(ServiceContext serviceContext)
        {
            return _PermissionRepository.GetAll(serviceContext);
        }

        public virtual PermissionModel? Get(string permissionKey)
        {
            return _PermissionRepository.Get(permissionKey);
        }

        public virtual PermissionModel? GetByPermissionKey(string permissionKey)
        {
            return _PermissionRepository.GetByPermissionKey(permissionKey);
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