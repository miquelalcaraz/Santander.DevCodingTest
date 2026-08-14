using Santander.DevCodingTest.Models;

namespace Santander.DevCodingTest.Contracts
{
    public interface IHackerNewsService
    {
        Task<IReadOnlyList<BestStoryResponse>> GetBestStoriesAsync(int count, CancellationToken cancellationToken = default);
    }
}
