using System.Text.Json;

using Santander.DevCodingTest.Services;

using FunctionalTests;

namespace FunctionalTests.HackerNewsServiceTesting;

public class GetBestStoriesAsync : IClassFixture<HackerNewsServiceFixture>
{
    private readonly HackerNewsServiceFixture _fixture;

    public GetBestStoriesAsync(HackerNewsServiceFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ReturnsStoriesOrderedByScoreDescending()
    {
        // Arrange

        // Act
        var result = await _fixture.GetService().GetBestStoriesAsync(3);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.True(result.FirstOrDefault()?.Score >= result.LastOrDefault()?.Score);

    }

    [Fact]
    public async Task RespectsCountParameter()
    {
        // Arrange

        // Act
        var result = await _fixture.GetService().GetBestStoriesAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }


}