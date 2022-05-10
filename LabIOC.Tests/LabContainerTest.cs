using FluentAssertions;
using Xunit;

namespace LabIOC.Tests;

public class LabContainerTest
{
    [Fact]
    public void GetMappings_EmptyFactory_IsEmpty()
    {
        var factory = LabContainerFactory.Create();
        var testee = factory.Build();
        testee.GetMappings().Should().BeEmpty();
    }

    [Fact]
    public void GetMappings_NotEmptyFactory_ContainsRegisteredTypes()
    {
        var factory = LabContainerFactory.Create()
            .Register(typeof(IocTestClass));
        var testee = factory.Build();
        testee.GetMappings().Should().ContainSingle(x => 
            x.ImplementationType == x.InterfaceType && x.ImplementationType == typeof(IocTestClass));
    }

    [Fact]
    public void GetMappings_ChangingFactoryAfterBuild_DoesNotChangeContainer()
    {
        var factory = LabContainerFactory.Create()
            .Register(typeof(IocTestClass));
        var testee = factory.Build();
        factory.Register(typeof(string));
        testee.GetMappings().Should().HaveCount(1);
    }

    [Fact]
    public void Get_RegisteredType_ThrowsException()
    {
        var factory = LabContainerFactory.Create()
            .Register(typeof(IocTestClass));
        var testee = factory.Build();
        Assert.Throws<TypeNotRegisteredException>(() =>
        {
            testee.Get(typeof(string));
        });
    }
    
    [Fact]
    public void Get_RegisteredType_Succeeds()
    {
        var factory = LabContainerFactory.Create()
            .Register(typeof(IocTestClass));
        var testee = factory.Build();
        testee.Get(typeof(IocTestClass));
    }

    [Fact]
    public void Get_NoPublicConstructor_ThrowsException()
    {
        var factory = LabContainerFactory.Create()
            .Register(typeof(NoPublicConstructor));
        var testee = factory.Build();
        Assert.Throws<NoSuitableConstructorFoundException>(() =>
        {
            testee.Get(typeof(NoPublicConstructor));
        });
    }

    [Fact]
    public void Get_TooManyConstructors_ThrowsException()
    {
        var factory = LabContainerFactory.Create()
            .Register(typeof(TooManyConstructors));
        var testee = factory.Build();
        Assert.Throws<NoSuitableConstructorFoundException>(() =>
        {
            testee.Get(typeof(TooManyConstructors));
        });
    }

    [Fact]
    public void Get_HierarchicalStructure_Succeeds()
    {
        var factory = LabContainerFactory.Create()
            .Register(typeof(Parent))
            .Register(typeof(Child));
        var testee = factory.Build();
        testee.Get(typeof(Parent));
    }

    [Fact]
    public void Get_WithGenerics_Succeeds()
    {
        var factory = LabContainerFactory.Create()
            .Register(typeof(IocTestClass));
        var testee = factory.Build();
        IocTestClass a = testee.Get<IocTestClass>();
        a.Should().NotBeNull();
    }
}