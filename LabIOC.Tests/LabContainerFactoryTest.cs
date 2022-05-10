using System;
using System.Linq;
using FluentAssertions;
using Moq;
using Xunit;

namespace LabIOC.Tests;

public class LabContainerFactoryTest
{
    private readonly LabContainerFactory _testee;

    public LabContainerFactoryTest()
    {
        _testee = LabContainerFactory.Create();
    }

    [Fact]
    public void Build_NothingRegistered_Succeeds()
    {
        _testee.Build();
    }

    [Fact]
    public void Build_ReturnsNewContainerInstance()
    {
        var container1 = _testee.Build();
        var container2 = _testee.Build();
        container1.Should().NotBe(container2);
    }

    [Fact]
    public void Register_ReturnsSameFactory()
    {
        var result = _testee.Register(typeof(IocTestClass));
        _testee.Should().Be(result);
    }

    [Fact]
    public void Register_Type_Succeeds()
    {
        _testee.Register(typeof(IocTestClass));
    }

    [Fact]
    public void Register_Interface_Succeeds()
    {
        _testee.Register(typeof(IIocTest), typeof(IocTestClass));
    }

    [Fact]
    public void GetMappings_NothingRegistered_Empty()
    {
        _testee.GetMappings().Should().BeEmpty();
    }

    [Fact]
    public void GetMappings_TypeRegistered_IsNotEmpty()
    {
        _testee.Register(typeof(IocTestClass));
        _testee.GetMappings().Should().NotBeEmpty();
    }

    [Fact]
    public void GetMappings_InterfaceRegistered_IsNotEmpty()
    {
        _testee.Register(typeof(IIocTest), typeof(IocTestClass));
        _testee.GetMappings().Should().NotBeEmpty();
    }

    [Fact]
    public void GetMappings_InterfaceRegistered_HasRegisteredTypes()
    {
        _testee.Register(typeof(IIocTest), typeof(IocTestClass));
        _testee.GetMappings().Should().ContainSingle(x =>
            x.InterfaceType.IsAssignableTo(typeof(IIocTest))
            && x.ImplementationType.IsAssignableTo(typeof(IocTestClass)));
    }

    [Fact]
    public void Register_UnimplementedInterface_ThrowsException()
    {
        Assert.Throws<InterfaceNotImplementedException>(() =>
        {
            _testee.Register(typeof(INotImplemented), typeof(IocTestClass));
        });
    }
    
    [Fact]
    public void Register_TypeWithGenerics_Succeeds()
    {
        var factory = LabContainerFactory.Create()
            .Register<IocTestClass>();
    }
    
    [Fact]
    public void Register_InterfaceWithGenerics_Succeeds()
    {
        var factory = LabContainerFactory.Create()
            .Register<IIocTest, IocTestClass>();
    }
}