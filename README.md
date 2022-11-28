# About
 LabIOC is a C# inversion of control framework which essentially means your classes 
 are more loosely coupled to one another, therefore, making them much easier to 
 test and maintain long-term.

# Usage/Examples

### Interface and implementation
```c#
public class Car : ICar
{
    private readonly IEngine _engine;
    private readonly ITurbo _turbo;

    public Car(IEngine engine, ITurbo turbo)
    {
        _engine = engine;
        _turbo = turbo;
    }

    public int CalculateHorsepower()
    {
        return (int) (_engine.EnginePower() * _turbo.TurboFactor());
    }

    public override string ToString()
    {
        return $"{CalculateHorsepower()}hp.";
    }
}

public interface ICar
{
    int CalculateHorsepower();
}


public class BasicEngine : IEngine
{
    public double EnginePower()
    {
        return 100;
    }
}

public class BigEngine : IEngine
{
    public double EnginePower()
    {
        return 200;
    }
}

public interface IEngine
{
    double EnginePower();
}


public class DefaultTurbo : ITurbo
{
    public double TurboFactor()
    {
        return 1;
    }
}

public class SuperTurbo : ITurbo
{
    public double TurboFactor()
    {
        return 1.25;
    }
}

public interface ITurbo
{
    double TurboFactor();
}
```
In this example, we are creating a car, of which we will be able to get the horsepower of.
We also have an engine object and a turbo object which every car has, and uses, to calculate
its horsepower. 

### Creating a container via the factory and adding our classes and interfaces 
```c#
var demoContainer = LabContainerFactory.Create()
                    .Register<IDemo, Demo>() // every object must implement an interface
                    .Build();

var baseCarContainer = LabContainerFactory.Create()
    .Register<ICar, Car>()
    .Register<IEngine, BasicEngine>()
    .Register<ITurbo, DefaultTurbo>()
    .Build();
```

### Getting data from a class
```c#
var car = baseCarContainer.Get<ICar>();

Console.WriteLine(car); // "100hp"
```

### Another Example
```c#
var fastCarContainer = LabContainerFactory.Create()
    .Register<ICar, Car>()
    .Register<IEngine, BigEngine>()
    .Register<ITurbo, SuperTurbo>()
    .Build();
    
var superCar = fastCarContainer.Get(ICar)();
Console.WriteLine(superCar); // "312hp"
```
This example is identical to the one above, but using different engine and turbo objects.

# License
[ MIT ](https://choosealicense.com/licenses/mit/)