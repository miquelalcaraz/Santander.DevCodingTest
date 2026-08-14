using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Santander.DevCodingTest;

namespace FunctionalTests.HackerNewsTopStoriesControllerTesting
{
    public class ControllerFixture : ServicesFixture
    {
        public const int PermitLimit = 3;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.PostConfigure<HackerNewsOptions>(options =>
                {
                    options.RateLimiting.PermitLimit = PermitLimit;
                    options.RateLimiting.Window = TimeSpan.FromMinutes(1);
                    options.RateLimiting.QueueLimit = 0;
                });
            });
        }
    }
}
 