using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MapResourcePostAttribute : Attribute
{
    public string Type { get; set; }

    public MapResourcePostAttribute(string type)
    {
        Type = type;
    }
}