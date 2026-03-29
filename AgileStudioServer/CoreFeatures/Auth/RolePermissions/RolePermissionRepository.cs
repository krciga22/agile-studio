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

        protected override DbSet<RolePermission> GetDbSet()
        {
            return _DBContext.RolePermission;
        }
    }
}