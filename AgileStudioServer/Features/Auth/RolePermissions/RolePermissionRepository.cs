using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Features.Auth.RolePermissions
{
    public class RolePermissionRepository(DBContext dbContext, Hydrator hydrator) :
        EntityRepository<DBContext, RolePermissionModel, RolePermission, object[]>(dbContext, hydrator)
    {
        public override object[] GetIdentifier(RolePermissionModel model)
        {
            return [model.RoleKey, model.PermissionKey, model.Scope];
        }

        /// <summary>
        /// Get all role permissions (permissions) assigned 
        /// to a given role.
        /// </summary>
        public List<RolePermissionModel> GetByRole(string roleKey, string? scope = null)
        {
            var query = GetDbSet().Where(rp => rp.RoleKey == roleKey);

            if(scope != null){
                query = query.Where(rp => rp.Scope == scope);
            }

            return HydrateModels([.. query]);
        }

        /// <summary>
        /// Get all role permissions (roles) assigned 
        /// to a given permission.
        /// </summary>
        public List<RolePermissionModel> GetByPermission(string permissionKey, string? scope = null)
        {
            var query = GetDbSet().Where(rp => rp.PermissionKey == permissionKey);

            if (scope != null){
                query = query.Where(rp => rp.Scope == scope);
            }

            return HydrateModels([.. query]);
        }

        /// <summary>
        /// Get all role permissions (roles and permissions) 
        /// assigned to a given scope.
        /// </summary>
        public List<RolePermissionModel> GetByScope(string scope)
        {
            var entities = GetDbSet().Where(rp => rp.Scope == scope);

            return HydrateModels([.. entities]);
        }

        protected override DbSet<RolePermission> GetDbSet()
        {
            return _DBContext.RolePermission;
        }
    }
}