using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FunctionalTests.HackerNewsTopStoriesControllerTesting
{
    public class GetBestStories : IClassFixture<ControllerFixture>
    {
        private readonly ControllerFixture _fixture;

        public GetBestStories(ControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ExceedingRateLimit_Returns429()
        {
            var client = _fixture.CreateClient();
            int permitLimit = 3;

            for (int i = 0; i < permitLimit; i++)
            {
                var response = await client.GetAsync("/api/hacker-news/stories/best?top=1");
                Assert.NotEqual(HttpStatusCode.TooManyRequests, response.StatusCode);
            }

            var rateLimited = await client.GetAsync("/api/hacker-news/stories/best?top=1");

            Assert.Equal(HttpStatusCode.TooManyRequests, rateLimited.StatusCode);
        }
    }
}
