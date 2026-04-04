using AgileStudioServer.Core.Hydrator;
using AgileStudioServer.Core.Repositories;
using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioServer.CoreFeatures.Auth.Permissions
{
    public class PermissionRepository(DBContext dbContext, Hydrator hydrator) :
        EntityRepository<DBContext, PermissionModel, Permission, string>(dbContext, hydrator)
    {
        public override string GetIdentifier(PermissionModel model)
        {
            return model.PermissionKey;
        }

        public List<PermissionModel> GetByScope(string scope)
        {
            var query = GetDbSet().Where(p => p.Scope == scope);
            return HydrateModels([.. query]);
        }

        protected override DbSet<Permission> GetDbSet()
        {
            return _DBContext.Permission;
        }
    }
}