[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MapResourcePatchAttribute : Attribute
{
    public string Type { get; }
    public MapResourcePatchAttribute(string type) => Type = type;
}