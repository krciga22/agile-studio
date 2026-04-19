[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MapResourceDeleteAttribute : Attribute
{
    public string Type { get; }
    public MapResourceDeleteAttribute(string type) => Type = type;
}