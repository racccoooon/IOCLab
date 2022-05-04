namespace LabIOC;

public class InterfaceNotImplementedException : Exception
{
    public InterfaceNotImplementedException(Type interfaceType, Type implementationType)
        : base($"Interface {interfaceType.Name} must be implemented by {implementationType.Name}.")
    {
        
    }
}