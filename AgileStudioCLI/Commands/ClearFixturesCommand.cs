using AgileStudioServer.Data;
using Microsoft.EntityFrameworkCore;

namespace AgileStudioCLI.Commands
{
    public class ClearFixturesCommand : AbstractCommand
    {
        private readonly DBContext _DBContext;

        public ClearFixturesCommand(DBContext dbContext)
        {
            _DBContext = dbContext;

            SetName("ClearFixtures");
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            RemoveAllFixtures();
        }

        private async void RemoveAllFixtures()
        {
            // delete project related entities
            await _DBContext.BacklogItem.ForEachAsync(x => _DBContext.BacklogItem.Remove(x));
            await _DBContext.Release.ForEachAsync(x => _DBContext.Release.Remove(x));
            await _DBContext.Sprint.ForEachAsync(x => _DBContext.Sprint.Remove(x));
            await _DBContext.Project.ForEachAsync(x => _DBContext.Project.Remove(x));

            // delete account related entities
            await _DBContext.BacklogItemTypeSchema.ForEachAsync(x => _DBContext.BacklogItemTypeSchema.Remove(x));
            await _DBContext.ChildBacklogItemType.ForEachAsync(x => _DBContext.ChildBacklogItemType.Remove(x));
            await _DBContext.BacklogItemType.ForEachAsync(x => _DBContext.BacklogItemType.Remove(x));
            await _DBContext.BacklogItemLinkTypeSchemaEntry.ForEachAsync(x => _DBContext.BacklogItemLinkTypeSchemaEntry.Remove(x));
            await _DBContext.BacklogItemLinkTypeSchema.ForEachAsync(x => _DBContext.BacklogItemLinkTypeSchema.Remove(x));
            await _DBContext.BacklogItemLinkType.ForEachAsync(x => _DBContext.BacklogItemLinkType.Remove(x));
            await _DBContext.WorkflowState.ForEachAsync(x => _DBContext.WorkflowState.Remove(x));
            await _DBContext.Workflow.ForEachAsync(x => _DBContext.Workflow.Remove(x));
            await _DBContext.Account.ForEachAsync(x => _DBContext.Account.Remove(x));
            await _DBContext.AccountType.ForEachAsync(x => _DBContext.AccountType.Remove(x));

            await _DBContext.RoleGrant.ForEachAsync(x => _DBContext.RoleGrant.Remove(x));
            await _DBContext.RolePermission.ForEachAsync(x => {
                if (!x.IsSystemRolePermission){
                    _DBContext.RolePermission.Remove(x);
                }
            });
            await _DBContext.Role.ForEachAsync(x => {
                if (!x.IsSystemRole){
                    _DBContext.Role.Remove(x);
                }
            });
            await _DBContext.Permission.ForEachAsync(x => {
                if (!x.IsSystemPermission){
                    _DBContext.Permission.Remove(x);
                }
            });
            await _DBContext.User.ForEachAsync(x => _DBContext.User.Remove(x));

            _DBContext.SaveChanges();
        }
    }
}
