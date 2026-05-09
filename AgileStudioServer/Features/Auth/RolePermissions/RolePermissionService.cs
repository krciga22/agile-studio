using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Features.Auth.RolePermissions
{
    public class RolePermissionService(RolePermissionRepository rolePermissionRepository) : AbstractService
    {
        private readonly RolePermissionRepository _RolePermissionRepository = rolePermissionRepository;

        public virtual RolePermissionModel? Get(string roleKey, string permissionKey, string scope = Scopes.Scopes.GLOBAL)
        {
            return _RolePermissionRepository.Get([roleKey, permissionKey, scope]);
        }

        public List<RolePermissionModel> GetByRole(string roleKey, string? scope = null)
        {
            return _RolePermissionRepository.GetByRole(roleKey, scope);
        }

        public List<RolePermissionModel> GetByPermission(string permissionKey, string? scope = null)
        {
            return _RolePermissionRepository.GetByPermission(permissionKey, scope);
        }

        public List<RolePermissionModel> GetByScope(string scope)
        {
            return _RolePermissionRepository.GetByScope(scope);
        }

        public virtual RolePermissionModel Create(RolePermissionModel model)
        {
            return _RolePermissionRepository.Create(model);
        }

        public virtual void Delete(RolePermissionModel model)
        {
            _RolePermissionRepository.Delete(model);
        }
    }
}