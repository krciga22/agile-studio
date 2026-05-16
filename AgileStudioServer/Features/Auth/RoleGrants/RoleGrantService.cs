using AgileStudioServer.Core.Services;

namespace AgileStudioServer.Features.Auth.RoleGrants
{
    public class RoleGrantService(RoleGrantRepository roleGrantRepository) : AbstractService
    {
        private readonly RoleGrantRepository _RoleGrantRepository = roleGrantRepository;

        public virtual RoleGrantModel? Get(int id)
        {
            return _RoleGrantRepository.Get(id);
        }

        public virtual List<RoleGrantModel> GetRoleGrants(
            string? roleKey, string? subjectType, string? subjectId,
            string? scope, string? scopeId)
        {
            return _RoleGrantRepository.GetRoleGrants(roleKey, subjectType, subjectId, scope, scopeId);
        }

        public virtual List<RoleGrantModel> GetRoleGrantsBySubjectAndScope(
            string subjectType, string subjectId, string scope, string? scopeId)
        {
            return _RoleGrantRepository.GetRoleGrantsBySubjectAndScope(subjectType, subjectId, scope, scopeId);
        }

        public virtual List<RoleGrantModel> GetRoleGrantsBySubjectScopeAndPermission(
            string subjectType, string subjectId, string scope, string? scopeId, string permissionKey)
        {
            return _RoleGrantRepository.GetRoleGrantsBySubjectScopeAndPermission(subjectType, subjectId, scope, scopeId, permissionKey);
        }

        public virtual RoleGrantModel Create(RoleGrantModel grant)
        {
            return _RoleGrantRepository.Create(grant);
        }

        public virtual RoleGrantModel Update(RoleGrantModel grant)
        {
            return _RoleGrantRepository.Update(grant);
        }

        public virtual void Delete(RoleGrantModel grant)
        {
            _RoleGrantRepository.Delete(grant);
        }
    }
}