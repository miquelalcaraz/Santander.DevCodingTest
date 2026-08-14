using Microsoft.Extensions.DependencyInjection;

using Santander.DevCodingTest.Services;

namespace FunctionalTests.HackerNewsApiClientTesting;

public class HackerNewsApiClientFixture : ServicesFixture
{
    public IHackerNewsApiClient GetClient() =>
        Services.GetRequiredService<IHackerNewsApiClient>();
}
