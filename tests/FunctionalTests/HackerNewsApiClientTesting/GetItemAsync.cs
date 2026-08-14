using Santander.DevCodingTest.Models;

namespace FunctionalTests.HackerNewsApiClientTesting;

public class GetItemAsync : IClassFixture<HackerNewsApiClientFixture>
{
    private const int KnownItemId = 1;

    private readonly HackerNewsApiClientFixture _fixture;

    public GetItemAsync(HackerNewsApiClientFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact]
    public async Task KnownItem_ReturnsNotNull()
    {
        var item = await _fixture.GetClient().GetItemAsync(KnownItemId);

        Assert.NotNull(item);
    }

    [Fact]
    public async Task KnownItem_HasExpectedId()
    {
        var item = await _fixture.GetClient().GetItemAsync(KnownItemId);

        Assert.Equal(KnownItemId, item!.Id);
    }

    [Fact]
    public async Task KnownItem_HasRequiredFields()
    {
        var item = await _fixture.GetClient().GetItemAsync(KnownItemId);

        Assert.NotNull(item);
        Assert.False(string.IsNullOrWhiteSpace(item.Type));
        Assert.False(string.IsNullOrWhiteSpace(item.Author));
        Assert.True(item.UnixTime > 0);
    }

    [Fact]
    public async Task TopStoryItem_HasScoreAndTitle()
    {
        var ids = await _fixture.GetClient().GetBestStoryIdsAsync();
        var topId = ids.First();

        var item = await _fixture.GetClient().GetItemAsync(topId);

        Assert.NotNull(item);
        Assert.False(string.IsNullOrWhiteSpace(item.Title));
        Assert.True(item.Score > 0);
    }

    [Fact]
    public async Task ReturnsCancellableRequest()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => _fixture.GetClient().GetItemAsync(KnownItemId, cts.Token));
    }
}
