using AgileStudioServer.Core.Pagination;
using AgileStudioServer.Core.Services;

namespace AgileStudioServer.CoreFeatures.Auth.Roles
{
    public class RoleService(RoleRepository roleRepository) : AbstractService
    {
        private readonly RoleRepository _RoleRepository = roleRepository;

        public virtual PaginationResults<RoleModel> GetAll(ServiceContext serviceContext)
        {
            return _RoleRepository.GetAll(serviceContext);
        }

        public virtual RoleModel? Get(string roleKey)
        {
            return _RoleRepository.Get(roleKey);
        }

        public virtual RoleModel? GetByUUID(string roleKey)
        {
            return _RoleRepository.GetByRoleKey(roleKey);
        }

        public virtual RoleModel Create(RoleModel role)
        {
            return _RoleRepository.Create(role);
        }

        public virtual RoleModel Update(RoleModel role)
        {
            return _RoleRepository.Update(role);
        }

        public virtual void Delete(RoleModel role)
        {
            _RoleRepository.Delete(role);
        }
    }
}
