using LabIOC;
using LabIOC.demo;

var labContainer = LabContainerFactory.Create()
    .Register<ICar, Car>()
    .Register<IEngine, CarEngine>()
    .Register<ITurbo, DefaultTurbo>()
    .Build();

var car = labContainer.Get<ICar>();

Console.WriteLine(car);
