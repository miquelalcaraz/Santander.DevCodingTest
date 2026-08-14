using Santander.DevCodingTest.Models;

namespace Santander.DevCodingTest.Services
{
    public interface IHackerNewsService
    {
        Task<IReadOnlyList<BestStoryResponse>> GetBestStoriesAsync(int count, CancellationToken cancellationToken = default);
    }
}
