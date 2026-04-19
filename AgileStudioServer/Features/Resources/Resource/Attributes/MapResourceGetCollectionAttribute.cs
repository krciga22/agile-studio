[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MapResourceGetCollectionAttribute : Attribute
{
    public string Type { get; }
    public MapResourceGetCollectionAttribute(string type) => Type = type;
}