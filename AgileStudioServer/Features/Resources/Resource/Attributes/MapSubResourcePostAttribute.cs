[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class MapSubResourcePostAttribute : Attribute
{
    public string Type { get; }

    public string SubType { get; }

    public string Path { get; }

    public MapSubResourcePostAttribute(string type, string subType, string path){
        Type = type;
        SubType = subType;
        Path = path;
    }
}