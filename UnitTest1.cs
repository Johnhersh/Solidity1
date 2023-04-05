using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;

namespace Step1;

public class UnitTest1: IDisposable
{
    private readonly IContainerService Docker;
    
    public UnitTest1()
    {
        const string ImageName = "ganache-testing";
        Docker = new Builder()
            .DefineImage(ImageName).ReuseIfAlreadyExists()
            .From("trufflesuite/ganache:v7.7.7")
            .Builder()

            // Container definition
            .UseContainer()
            .WithName(Guid.NewGuid().ToString("D").ToLower())
            .UseImage(ImageName)

            .Build()
            .Start();
    }
    
    public void Dispose()
    {
        Docker.Dispose();
    }
    
    [Fact]
    public void Test1()
    {

    }
}