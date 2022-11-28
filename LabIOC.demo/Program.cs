using LabIOC;
using LabIOC.demo;

var labContainer = LabContainerFactory.Create()
    .Register<ICar, Car>()
    .Register<IEngine, BasicEngine>()
    .Register<ITurbo, DefaultTurbo>()
    .Build();

var car = labContainer.Get<ICar>();

Console.WriteLine(car);

var fastCarContainer = LabContainerFactory.Create()
    .Register<ICar, Car>()
    .Register<IEngine, BigEngine>()
    .Register<ITurbo, SuperTurbo>()
    .Build();
    
var superCar = fastCarContainer.Get<ICar>();
Console.WriteLine(superCar);