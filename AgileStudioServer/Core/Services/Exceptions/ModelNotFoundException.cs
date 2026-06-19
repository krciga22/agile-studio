using AgileStudioServer.Core.Exceptions;

namespace AgileStudioServer.Core.Services.Exceptions
{
    public class ModelNotFoundException : AbstractException
    {
        public string ModelClassName { get => modelClassName; }

        public string PrimaryKey { get => primaryKey; }

        public ModelNotFoundException(string modelClassName, string primaryKey) : base($"Required model \"{modelClassName}\" with primaryKey \"{primaryKey}\" not found")
        {
            this.modelClassName = modelClassName;
            this.primaryKey = primaryKey;
        }

        public static ModelNotFoundException FromCompositeKey(string modelClassName, object[] primaryKey)
        {
            return new ModelNotFoundException(modelClassName, string.Join(", ", primaryKey));
        }

        private string modelClassName;

        private string primaryKey;
    }
}
