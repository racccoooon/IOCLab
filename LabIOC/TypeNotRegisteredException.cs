namespace LabIOC;

public class TypeNotRegisteredException : Exception
{
    public TypeNotRegisteredException(Type requestedType)
        : base($"Type {requestedType.Name} has not been registered.")
    {
    }
}