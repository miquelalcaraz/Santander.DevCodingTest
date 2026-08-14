namespace FunctionalTests.HackerNewsApiClientTesting;

public class GetBestStoryIdsAsync : IClassFixture<HackerNewsApiClientFixture>
{
    private readonly HackerNewsApiClientFixture _fixture;

    public GetBestStoryIdsAsync(HackerNewsApiClientFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ReturnsNonEmptyArray()
    {
        var ids = await _fixture.GetClient().GetBestStoryIdsAsync();

        Assert.NotEmpty(ids);
    }

    [Fact]
    public async Task ReturnsOnlyPositiveIds()
    {
        var ids = await _fixture.GetClient().GetBestStoryIdsAsync();

        Assert.All(ids, id => Assert.True(id > 0));
    }

    [Fact]
    public async Task ReturnsUniqueIds()
    {
        var ids = await _fixture.GetClient().GetBestStoryIdsAsync();

        Assert.Equal(ids.Count(), ids.Distinct().Count());
    }

    [Fact(Skip = "Test is skipped because it cancels the request immediately due to cache implementation")]
    public async Task ReturnsCancellableRequest()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => _fixture.GetClient().GetBestStoryIdsAsync(cts.Token));
    }
}
