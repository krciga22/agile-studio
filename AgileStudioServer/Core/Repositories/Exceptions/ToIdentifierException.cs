using AgileStudioServer.Core.Exceptions;

namespace AgileStudioServer.Core.Repositories.Exceptions
{
    public class ToIdentifierException : AbstractException
    {
        private static string FormatMessage(Type identifierType) =>
            $"Failed to convert object array (object[]) to identifier of type {identifierType.FullName}.";

        public ToIdentifierException(Type identifierType)
            : base(FormatMessage(identifierType))
        {
        }

        public ToIdentifierException(Type identifierType, Exception? innerException)
            : base(FormatMessage(identifierType), innerException)
        {
        }
    }
}
