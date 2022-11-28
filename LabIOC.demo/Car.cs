namespace LabIOC.demo;

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
        return 250;
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