using System;

namespace LabIOC.Tests;

public interface IIocTest
{
}

public interface INotImplemented
{
}

public class IocTestClass : IIocTest
{
}

public class NoPublicConstructor
{
    private NoPublicConstructor()
    {
        
    }
}

public class Parent
{
    public Parent(Child child)
    {
    }
}

public class Child
{
}

public class TooManyConstructors
{
    public TooManyConstructors()
    {
        
    }

    public TooManyConstructors(bool b)
    {
        
    }
}