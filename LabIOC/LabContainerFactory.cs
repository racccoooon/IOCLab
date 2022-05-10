namespace LabIOC;

public class LabContainerFactory
{
    private readonly List<IocMapping> _registeredTypes = new ();

    private LabContainerFactory()
    {
    }

    public static LabContainerFactory Create()
    {
        return new LabContainerFactory();
    }

    public LabContainer Build()
    {
        return new LabContainer(_registeredTypes);
    }

    public LabContainerFactory Register(Type type)
    {
        return Register(type, type);
    }
    
    public LabContainerFactory Register<TType>()
    {
        return Register(typeof(TType), typeof(TType));
    }
    
    public LabContainerFactory Register<TInterface, TType>()
    {
        return Register(typeof(TInterface), typeof(TType));
    }

    public LabContainerFactory Register(Type interfaceType, Type implementationType)
    {
        if (!implementationType.IsAssignableTo(interfaceType))
            throw new InterfaceNotImplementedException(interfaceType, implementationType);
        _registeredTypes.Add(new IocMapping(interfaceType, implementationType));
        return this;
    }

    public IEnumerable<IocMapping> GetMappings()
    {
        return _registeredTypes;
    }
}