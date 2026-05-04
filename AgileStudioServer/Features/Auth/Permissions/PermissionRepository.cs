using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.Features.Auth.Permissions
{
    public class PermissionRepository(DBContext dbContext, Hydrator hydrator) :
        EntityRepository<DBContext, PermissionModel, Permission, string>(dbContext, hydrator)
    {
        public override string GetIdentifier(PermissionModel model)
        {
            return model.PermissionKey;
        }

        protected override DbSet<Permission> GetDbSet()
        {
            return _DBContext.Permission;
        }
    }
}