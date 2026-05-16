using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Features.Auth.RoleGrants
{
    public class RoleGrantRepository(DBContext dbContext, Hydrator hydrator) :
        EntityRepository<DBContext, RoleGrantModel, RoleGrant, int>(dbContext, hydrator)
    {
        public override int GetIdentifier(RoleGrantModel model)
        {
            return model.ID;
        }

        public List<RoleGrantModel> Get(string roleKey)
        {
            var query = GetDbSet().Where(g => g.RoleKey == roleKey);
            return HydrateModels([.. query]);
        }

        public List<RoleGrantModel> GetRoleGrants(
            string? roleKey, string? subjectType, string? subjectId, 
            string? scope, string? scopeId)
        {
            if(roleKey == null 
                && subjectType == null 
                && subjectId == null 
                && scope == null 
                && scopeId == null){
                throw new ArgumentException("At least one argument must be provided");
            }

            var query = GetDbSet().AsQueryable();

            if (roleKey != null){
                query = query.Where(g => g.RoleKey == roleKey);
            }

            if (subjectType != null){
                query = query.Where(g => g.SubjectType == subjectType);
            }

            if (subjectId != null){
                query = query.Where(g => g.SubjectID == subjectId);
            }

            if (scope != null){
                query = query.Where(g => g.Scope == scope);
            }

            if (scopeId != null){
                query = query.Where(g => g.ScopeID == scopeId);
            }

            return HydrateModels([.. query]);
        }

        public List<RoleGrantModel> GetRoleGrantsBySubjectAndScope(
            string subjectType, string subjectId, string scope,
            string? scopeId)
        {
            var query = from rg in _DBContext.RoleGrant
                        where rg.SubjectType == subjectType
                              && rg.SubjectID == subjectId
                              && rg.Scope == scope
                              && rg.ScopeID == scopeId
                        select rg;

            return HydrateModels([.. query]);
        }

        public List<RoleGrantModel> GetRoleGrantsBySubjectScopeAndPermission(
            string subjectType, string subjectId, string scope, 
            string? scopeId, string permissionKey)
        {
            var query = from rg in _DBContext.RoleGrant
                        join rp in _DBContext.RolePermission on rg.RoleKey equals rp.RoleKey
                        join p in _DBContext.Permission on rp.PermissionKey equals p.PermissionKey
                        where rg.SubjectType == subjectType
                              && rg.SubjectID == subjectId
                              && rg.Scope == scope
                              && rg.ScopeID == scopeId
                              && rp.Scope == scope
                              && p.PermissionKey == permissionKey
                        select rg;

            return HydrateModels([.. query]);
        }

        protected override DbSet<RoleGrant> GetDbSet()
        {
            return _DBContext.RoleGrant;
        }
    }
}