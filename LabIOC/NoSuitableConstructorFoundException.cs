namespace LabIOC;

public class NoSuitableConstructorFoundException : Exception
{
    public NoSuitableConstructorFoundException(Type type)
    : base($"Type {type.Name} has no suitable constructors.")
    {
    }
}