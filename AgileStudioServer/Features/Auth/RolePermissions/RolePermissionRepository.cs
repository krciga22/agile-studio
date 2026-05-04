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