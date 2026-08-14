using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Santander.DevCodingTest;
using Santander.DevCodingTest.Services;

namespace FunctionalTests;

public abstract class ServicesFixture : WebApplicationFactory<Program>, IAsyncLifetime
{ 

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        { 
        });
    }



    public virtual ValueTask InitializeAsync() => ValueTask.CompletedTask;
}