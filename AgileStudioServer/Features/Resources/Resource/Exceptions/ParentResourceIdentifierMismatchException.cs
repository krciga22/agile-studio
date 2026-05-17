namespace AgileStudioServer.Features.Resources.Resource.Exceptions
{
    public class ParentResourceIdentifierMismatchException : Exception
    {
        public ParentResourceIdentifierMismatchException(object[] id) : 
            base($"The parent resource identifier ({id}) given doesn't match the given resource")
        {

        }
    }
}
