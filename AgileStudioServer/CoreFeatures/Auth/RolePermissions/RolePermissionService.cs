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