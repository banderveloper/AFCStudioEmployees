namespace AFCStudioEmployees.Application.Extensions;

public static class QueryableExtensions
{
    // Extension method to sort entities by property name using reflection
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> query, string propertyName)
    {
        var entityType = typeof(T);
        var propertyInfo = entityType.GetProperty(propertyName);

        if (propertyInfo is null)
            throw new ArgumentException($"Property {propertyName} not found on type {entityType.Name}");

        return query.OrderBy(e => propertyInfo.GetValue(e, null));
    }
}