using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Auth.RolePermissions
{
    public class RolePermissionRepository(DBContext dbContext, Hydrator hydrator) :
        EntityRepository<DBContext, RolePermissionModel, RolePermission, string[]>(dbContext, hydrator)
    {
        public override string[] GetIdentifier(RolePermissionModel model)
        {
            return [model.RoleKey, model.PermissionKey];
        }

        /// <summary>
        /// Get all role permissions (permissions) assigned 
        /// to a given role.
        /// </summary>
        public List<RolePermissionModel> GetByRole(string roleKey)
        {
            var entities = GetDbSet().Where(rp => rp.RoleKey == roleKey);
            return HydrateModels([.. entities]);
        }

        /// <summary>
        /// Get all role permissions (roles) assigned 
        /// to a given permission.
        /// </summary>
        public List<RolePermissionModel> GetByPermission(string permissionKey)
        {
            var entities = GetDbSet().Where(rp => rp.PermissionKey == permissionKey);
            return HydrateModels([.. entities]);
        }

        /// <summary>
        /// Get all role permissions (roles and permissions) 
        /// assigned to a given scope and scope ID.
        /// </summary>
        public List<RolePermissionModel> GetByScope(
            string scope, string? scopeId)
        {
            var entities = GetDbSet().Where(rp => rp.Role.Scope == scope)
                .Where(rp => rp.Role.ScopeID == scopeId);

            return HydrateModels([.. entities]);
        }

        protected override DbSet<RolePermission> GetDbSet()
        {
            return _DBContext.RolePermission;
        }
    }
}