using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Nethereum.Web3.Accounts;

namespace Step1;

public class UnitTest1: IDisposable
{
    private readonly IContainerService Docker;
    
    public UnitTest1()
    {
        var EcKey = Nethereum.Signer.EthECKey.GenerateKey();
        var Account = new Account(EcKey, 1337);
        
        const string ImageName = "ganache-testing";
        Docker = new Builder()
            .DefineImage(ImageName).ReuseIfAlreadyExists()
            .From("trufflesuite/ganache:v7.7.7")
            .Builder()

            // Container definition
            .UseContainer()
            .WithName(Guid.NewGuid().ToString("D").ToLower())
            .UseImage(ImageName)
            .Command($"""--wallet.accounts "{Account.PrivateKey},1000000000000000000000" """)

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