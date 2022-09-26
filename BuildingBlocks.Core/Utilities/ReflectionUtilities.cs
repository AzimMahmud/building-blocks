using System.Linq.Expressions;
using System.Reflection;

namespace BuildingBlocks.Core.Utilities;

public class ReflectionUtilities
{
    private static dynamic CreateGenericType(Type genericType, Type[] typeArguments, params object?[] constructorArgs)
    {
        var type = genericType.MakeGenericType(typeArguments);
        return Activator.CreateInstance(type, constructorArgs);
    }

    public static dynamic CreateGenericType<TGenericType>(Type[] typeArguments, params object?[] constructorArgs)
    {
        return CreateGenericType(typeof(TGenericType), typeArguments, constructorArgs);
    }

    public static IEnumerable<Type> GetAllTypesImplementingInterface<TInterface>(params Assembly[] assemblies)
    {
        var inputAssemblies = assemblies.Any() ? assemblies : AppDomain.CurrentDomain.GetAssemblies();
        return inputAssemblies.SelectMany(GetAllTypesImplementingInterface<TInterface>);
    }

    private static IEnumerable<Type> GetAllTypesImplementingInterface<TInterface>(Assembly? assembly = null)
    {
        var inputAssembly = assembly ?? Assembly.GetExecutingAssembly();
        return inputAssembly.GetTypes().Where(type =>
            typeof(TInterface).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract && type.IsClass);
    }

    public static IEnumerable<string?> GetPropertyNames<T>(params Expression<Func<T, object>>?[] propertyExpressions)
    {
        var values = new List<string?>();

        foreach (var propertyExpression in propertyExpressions)
        {
            values.Add(GetPropertyName(propertyExpression));
        }

        return values;
    }

    private static string? GetPropertyName<T>(Expression<Func<T, object>>? propertyExpression)
    {
        string? value = null;

        if (propertyExpression is not null)
        {
            var lambda = (LambdaExpression)propertyExpression;

            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression unaryExpression)
            {
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            value = memberExpression.Member.Name;
        }

        return value;
    }

    public static Type? GetTypeFromAnyReferencingAssembly(string typeName)
    {
        var referencedAssemblies = Assembly.GetEntryAssembly()?
            .GetReferencedAssemblies()
            .Select(a => a.FullName);

        if (referencedAssemblies is null)
        {
            return null;
        }

        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => referencedAssemblies.Contains(a.FullName))
            .SelectMany(a => a.GetTypes().Where(t => t.FullName == typeName || t.Name == typeName))
            .FirstOrDefault();
    }

    public static Type? GetFirstMatchingTypeFromCurrentDomainAssemblies(string typeName)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes().Where(t => t.FullName == typeName || t.Name == typeName))
            .FirstOrDefault();
    }

    public static Type? GetFirstMatchingTypeFromAssembly(string typeName, Assembly assembly)
    {
        return assembly.GetTypes()
            .FirstOrDefault(t => t.FullName == typeName || t.Name == typeName);
    }
}