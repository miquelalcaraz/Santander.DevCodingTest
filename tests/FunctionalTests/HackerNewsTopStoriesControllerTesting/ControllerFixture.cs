using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Hosting;

namespace FunctionalTests.HackerNewsTopStoriesControllerTesting
{
    public class ControllerFixture : ServicesFixture
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
             
        }
    }
}
 