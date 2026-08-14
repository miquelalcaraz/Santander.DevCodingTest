using System;
using System.Collections.Generic;
using System.Text;

using Santander.DevCodingTest.Services;

namespace FunctionalTests.HackerNewsServiceTesting
{

    public class GetStoryAsync : IClassFixture<HackerNewsServiceFixture>
    {
        private readonly HackerNewsServiceFixture _fixture;

        public GetStoryAsync(HackerNewsServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ReturnsStoriesOrderedByScoreDescending()
        {
            var result = await _fixture.GetService().GetBestStoriesAsync(3);

        }

        [Fact(Skip = "Not implemented yet")]
        public async Task GetStory_returnsStory()
        {
            throw new NotImplementedException();
        }


    }
}
