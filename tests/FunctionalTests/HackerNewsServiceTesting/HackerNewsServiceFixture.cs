using Microsoft.Extensions.DependencyInjection;

using Santander.DevCodingTest.Services;

namespace FunctionalTests.HackerNewsServiceTesting;

public class HackerNewsServiceFixture : ServicesFixture
{
    public IHackerNewsService GetService() => Services.GetRequiredService<IHackerNewsService>();
}
