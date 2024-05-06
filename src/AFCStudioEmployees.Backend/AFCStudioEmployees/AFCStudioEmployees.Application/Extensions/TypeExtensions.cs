namespace AFCStudioEmployees.Application.Extensions;

public static class TypeExtensions
{
    // Whether object contains property with given property name
    public static bool HasProperty(this Type type, string propertyName)
    {
        return type.GetProperties().Any(pi => pi.Name.Equals(propertyName));
    }
}