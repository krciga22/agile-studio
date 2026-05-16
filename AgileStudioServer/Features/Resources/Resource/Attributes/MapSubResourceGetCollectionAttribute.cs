[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class MapSubResourceGetCollectionAttribute : Attribute
{
    public string Type { get; }

    public string SubType { get; }

    public string Path { get; }

    public MapSubResourceGetCollectionAttribute(string type, string subType, string path){
        Type = type;
        SubType = subType;
        Path = path;
    }
}