namespace AgileStudioServer.Features.Auth.Permissions
{
    public class PermissionKeys
    {
        // general/collection level permissions
        public const string CREATE = "create";
        public const string LIST = "list";
        public const string BULK_UPDATE = "bulk-update";
        public const string BULK_DELETE = "bulk-delete";

        // record level permissions
        public const string READ = "read";
        public const string UPDATE = "update";
        public const string DELETE = "delete";
    }
}