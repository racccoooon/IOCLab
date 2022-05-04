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
        throw new NotImplementedException();
    }
}