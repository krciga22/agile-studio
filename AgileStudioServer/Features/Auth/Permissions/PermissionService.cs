using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Features.Auth.Permissions
{
    public class PermissionService(PermissionRepository permissionRepository) : AbstractService
    {
        private readonly PermissionRepository _PermissionRepository = permissionRepository;

        public virtual PermissionModel? Get(string permissionKey)
        {
            return _PermissionRepository.Get(permissionKey);
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