using Santander.DevCodingTest.Models;

namespace Santander.DevCodingTest.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly IHackerNewsApiClient _apiClient;

        public HackerNewsService(IHackerNewsApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public async Task<IReadOnlyList<BestStoryResponse>> GetBestStoriesAsync(int count, CancellationToken cancellationToken = default)
        {
            if (count < 1)
                count = 10;

            var storyIds = await _apiClient.GetBestStoryIdsAsync(cancellationToken);

            var stories = new System.Collections.Concurrent.ConcurrentBag<HackerNewsItem>();
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 10,
                CancellationToken = cancellationToken
            };
            await Parallel.ForEachAsync(storyIds, parallelOptions,
                async (id, ct) =>
                {
                    var item = await _apiClient.GetItemAsync(id, ct);
                    if (item is not null)
                        stories.Add(item);
                });

            return stories
                .Select(BestStoryResponse.From)
                .OrderByDescending(s => s.Score)
                .Take(count)
                .ToArray();
        }

    }

}

