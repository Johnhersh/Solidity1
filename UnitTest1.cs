using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Extensions;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace Step1;

public class UnitTest1: IAsyncLifetime
{
    private readonly IContainerService Docker;
    private readonly Web3 Web3;
    
    public UnitTest1()
    {
        var EcKey = Nethereum.Signer.EthECKey.GenerateKey();
        var Account = new Account(EcKey, 1337);
        
        const string ImageName = "ganache-testing";
        Docker = new Builder()
            .DefineImage(ImageName).ReuseIfAlreadyExists()
            .From("trufflesuite/ganache:v7.7.7")
            .ExposePorts(8545)
            .Builder()

            // Container definition
            .UseContainer()
            .WithName(Guid.NewGuid().ToString("D").ToLower())
            .UseImage(ImageName)
            .ExposePort(8545)
            .Command($"""--wallet.accounts "{Account.PrivateKey},1000000000000000000000" """)

            .Build()
            .Start();

        var ExposedPort = Docker.ToHostExposedEndpoint("8545/tcp").Port;
        Web3 = new Web3(Account, $"http://localhost:{ExposedPort}");
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        Docker.Dispose();
        return Task.CompletedTask;
    }
    
    [Fact]
    public void Test1()
    {

    }
}