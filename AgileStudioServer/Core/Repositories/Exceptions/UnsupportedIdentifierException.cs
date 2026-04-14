using AgileStudioServer.Core.Exceptions;

namespace AgileStudioServer.Core.Repositories.Exceptions
{
    public class UnsupportedIdentifierException : AbstractException
    {
        private static string FormatMessage(Type identifierType) =>
            $"The given identifier is not yet supported: {identifierType.FullName}.";

        public UnsupportedIdentifierException(Type identifierType) :
            base(FormatMessage(identifierType))
        {
        }

        public UnsupportedIdentifierException(Type identifierType, Exception? innerException) :
            base(FormatMessage(identifierType), innerException)
        {
        }
    }
}
