using Ductus.FluentDocker.Builders;

namespace Step1;

public class UnitTest1
{
    public UnitTest1()
    {
        var Docker = new Builder()
            .DefineImage("ganache-testing").ReuseIfAlreadyExists()
            .From("trufflesuite/ganache:v7.7.7")
            .Builder().Build();
    }
    
    [Fact]
    public void Test1()
    {

    }
}