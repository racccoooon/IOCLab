using BindingFlags = System.Reflection.BindingFlags;

namespace LabIOC;

public class LabContainer
{
    private readonly Dictionary<Type, Type> _mappings;

    internal LabContainer(IEnumerable<IocMapping> mappings)
    {
        _mappings = mappings.ToDictionary(x => x.InterfaceType,
            x => x.ImplementationType);
    }

    public IEnumerable<IocMapping> GetMappings()
    {
        return _mappings.Keys.Select(x => new IocMapping(x, _mappings[x]));
    }

    public TInterface Get<TInterface>()
    {
        return (TInterface) Get(typeof(TInterface));
    }
    
    public object Get(Type type)
    {
        if (!_mappings.TryGetValue(type, out var mapping))
            throw new TypeNotRegisteredException(type);

        if (mapping.GetConstructors().Count() > 1)
            throw new NoSuitableConstructorFoundException(type);
        
        var constructor = mapping.GetConstructors().FirstOrDefault()
            ?? throw new NoSuitableConstructorFoundException(type);

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