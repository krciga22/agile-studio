using AgileStudioServer.Core.Exceptions;

namespace AgileStudioServer.Core.Repositories.Exceptions
{
    public class EntityNotFoundException : AbstractException
    {
        public EntityNotFoundException(string entityClassName, string primaryKey) : base($"Required entity \"{entityClassName}\" with primaryKey \"{primaryKey}\" not found")
        {

        }
    }
}
