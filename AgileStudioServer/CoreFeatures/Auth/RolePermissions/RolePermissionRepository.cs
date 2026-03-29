using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Auth.RolePermissions
{
    public class RolePermissionRepository(DBContext dbContext, Hydrator hydrator) :
        EntityRepository<DBContext, RolePermissionModel, RolePermission, int>(dbContext, hydrator)
    {
        public override int GetIdentifier(RolePermissionModel model)
        {
            return model.ID;
        }

        /// <summary>
        /// Get all role permissions (permissions) assigned 
        /// to a given role.
        /// </summary>
        public List<RolePermissionModel> GetByRole(
            int roleId, string? scope, int? scopeId)
        {
            var entities = GetDbSet().Where(rp => rp.RoleID == roleId);

            if(!string.IsNullOrWhiteSpace(scope)){
                entities = entities.Where(rp => rp.Scope == scope);
            }

            if(scopeId.HasValue){
                entities = entities.Where(rp => rp.ScopeID == scopeId.Value);
            }

            return HydrateModels([.. entities]);
        }

        /// <summary>
        /// Get all role permissions (roles) assigned 
        /// to a given permission.
        /// </summary>
        public List<RolePermissionModel> GetByPermission(
            int permissionId, string? scope, int? scopeId)
        {
            var entities = GetDbSet().Where(rp => rp.PermissionID == permissionId);

            if (!string.IsNullOrWhiteSpace(scope)){
                entities = entities.Where(rp => rp.Scope == scope);
            }

            if (scopeId.HasValue){
                entities = entities.Where(rp => rp.ScopeID == scopeId.Value);
            }

            return HydrateModels([.. entities]);
        }

        /// <summary>
        /// Get all role permissions (roles and permissions) 
        /// assigned to a given scope and scope ID.
        /// </summary>
        public List<RolePermissionModel> GetByScope(
            string scope, int? scopeId)
        {
            var entities = GetDbSet().Where(rp => rp.Scope == scope);

            if (scopeId.HasValue){
                entities = entities.Where(rp => rp.ScopeID == scopeId.Value);
            }

            return HydrateModels([.. entities]);
        }

        protected override DbSet<RolePermission> GetDbSet()
        {
            return _DBContext.RolePermission;
        }
    }
}