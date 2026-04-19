[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MapResourceGetAttribute : Attribute
{
    public string Type { get; set; }

    public MapResourceGetAttribute(string type)
    {
        Type = type;
    }
}