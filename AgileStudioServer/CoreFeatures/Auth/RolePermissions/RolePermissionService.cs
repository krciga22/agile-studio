using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Auth.RolePermissions
{
    public class RolePermissionService(RolePermissionRepository rolePermissionRepository) : AbstractService
    {
        private readonly RolePermissionRepository _RolePermissionRepository = rolePermissionRepository;

        public virtual RolePermissionModel? Get(int id)
        {
            return _RolePermissionRepository.Get(id);
        }

        public List<RolePermissionModel> GetByRole(int roleId)
        {
            return _RolePermissionRepository.GetByRole(roleId);
        }

        public List<RolePermissionModel> GetByPermission(int permissionId)
        {
            return _RolePermissionRepository.GetByPermission(permissionId);
        }

        public List<RolePermissionModel> GetByScope(string scope, int? scopeId)
        {
            return _RolePermissionRepository.GetByScope(scope, scopeId);
        }

        public virtual RolePermissionModel Create(RolePermissionModel model)
        {
            return _RolePermissionRepository.Create(model);
        }

        public virtual RolePermissionModel Update(RolePermissionModel model)
        {
            return _RolePermissionRepository.Update(model);
        }

        public virtual void Delete(RolePermissionModel model)
        {
            _RolePermissionRepository.Delete(model);
        }
    }
}