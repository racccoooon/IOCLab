using LabIOC;
using LabIOC.demo;

var labContainer = LabContainerFactory.Create()
    .Register(typeof(ICar), typeof(Car))
    .Register(typeof(IEngine), typeof(CarEngine))
    .Register(typeof(ITurbo), typeof(DefaultTurbo))
    .Build();

var car = labContainer.Get(typeof(ICar));

Console.WriteLine(car);

