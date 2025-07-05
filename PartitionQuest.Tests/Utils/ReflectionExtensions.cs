using System.Reflection;

namespace PartitionQuest.Tests.Utils;

public static class ReflectionExtensions
{
    /// <summary>
    /// Создаёт экземпляры всех классов, наследующих/реализующих указанный тип.
    /// </summary>
    public static List<TBase> CreateAllDerivedInstances<TBase>(this Assembly assembly)
    {
        var baseType = typeof(TBase);
        var types = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && baseType.IsAssignableFrom(t))
            .ToList();

        var instances = new List<TBase>();

        foreach (var type in types)
        {
            var ctor = type.GetConstructors().MinBy(c => c.GetParameters().Length);
            if (ctor == null) 
                continue;

            var args = ctor.GetParameters()
                .Select(p => GetDefault(p.ParameterType))
                .ToArray();

            if (Activator.CreateInstance(type, args) is TBase instance)
                instances.Add(instance);
        }

        return instances;
    }

    private static object? GetDefault(Type t) =>
        t.IsValueType ? Activator.CreateInstance(t) : null;
}