using Airports.Api.Test.TestHelpers;
using Airports.BusinessLayer.Services;
using Airports.Shared.Models;
using Airports.Shared.Static;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Xunit.Abstractions;

namespace Airports.Api.Test;

public abstract class TestServiceBase : RepositoryHelper
{
    private readonly ITestOutputHelper output;

    public TestServiceBase(ITestOutputHelper output) => this.output = output;

    protected ITestOutputHelper Output => output;

    private IConfigurationSection GetIConfigSection(string section)
        => new ConfigurationBuilder().AddJsonFile("appsettings.Development.json", optional: false).Build().GetSection(section);

    protected string GetTeleportHost() => GetIConfigSection("TeleportHost")[nameof(ConfigBase.Url)];

    protected IOptions<ConfigBase> GetTeleportHostConfigurations()
    {
        var teleportHost = GetTeleportHost();
        var options = new Mock<IOptions<ConfigBase>>();
        options.SetupGet(expr => expr.Value).Returns(
            new ConfigBase
            {
                Url = teleportHost,
            });

        return options.Object;
    }

    protected HttpService GetIHttpService()
    {
        var services = new ServiceCollection();
        var options = GetTeleportHostConfigurations();

        services.AddHttpClient(StaticItems.airportsHttpClient).ConfigureHttpClient(x =>
        {
            x.Timeout = TimeSpan.FromMinutes(100);
            x.BaseAddress = new Uri(options.Value.Url);
        });

        services.AddTransient<HttpService>();
        var servicesProvider = services.BuildServiceProvider();
        var httpFactory = servicesProvider.GetRequiredService<IHttpClientFactory>();
        var httpService = new HttpService(httpFactory, options);

        return httpService;
    }
}
