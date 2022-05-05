using BindingFlags = System.Reflection.BindingFlags;

namespace LabIOC;

public class LabContainer
{
    private readonly List<IocMapping> _mappings;

    internal LabContainer(IEnumerable<IocMapping> mappings)
    {
        _mappings = mappings.ToList();
    }

    public IEnumerable<IocMapping> GetMappings()
    {
        return _mappings;
    }

    public object Get(Type type)
    {
        var mapping = _mappings.FirstOrDefault(x => x.InterfaceType == type);
        if (mapping == null)
            throw new TypeNotRegisteredException(type);

        if (mapping.ImplementationType.GetConstructors().Count() > 1)
            throw new NoSuitableConstructorFoundException(type);
        
        var constructor = mapping.ImplementationType.GetConstructors().FirstOrDefault();
        if (constructor == null)
            throw new NoSuitableConstructorFoundException(type);

        var parameters = new List<object>();
        foreach (var parameterInfo in constructor.GetParameters())
        {
            parameters.Add(Get(parameterInfo.ParameterType));
        }
        
        var result = constructor.Invoke(parameters.ToArray());
        return result;
        // return new IocTestClass();
    }
}