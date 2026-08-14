using Santander.DevCodingTest.Models;

namespace Santander.DevCodingTest.Contracts
{
    public interface IHackerNewsApiClient
    {
        Task<IEnumerable<int>> GetBestStoryIdsAsync(CancellationToken cancellationToken = default);
        Task<HackerNewsItem?> GetItemAsync(int id, CancellationToken cancellationToken = default);
    }
}
