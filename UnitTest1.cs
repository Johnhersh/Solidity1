using Ductus.FluentDocker.Builders;

namespace Step1;

public class UnitTest1
{
    public UnitTest1()
    {
        const string ImageName = "ganache-testing";
        var Docker = new Builder()
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
    
    [Fact]
    public void Test1()
    {

    }
}