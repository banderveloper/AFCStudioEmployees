using System.Linq.Expressions;

namespace AFCStudioEmployees.Application.Extensions;

public static class QueryableExtensions
{
    // Extension method to sort entities by property name using reflection
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, bool ascending = true)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);

        var methodName = ascending ? "OrderBy" : "OrderByDescending";
        var types = new Type[] { source.ElementType, property.Type };

        var methodCall = Expression.Call(
            typeof(Queryable),
            methodName,
            types,
            source.Expression,
            Expression.Quote(lambda)
        );

        return source.Provider.CreateQuery<T>(methodCall);
    }
}