using Microsoft.Extensions.Options;

using Santander.DevCodingTest.Contracts;
using Santander.DevCodingTest.Models;

namespace Santander.DevCodingTest.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly IHackerNewsApiClient _apiClient;
        private readonly HackerNewsOptions _options;

        public HackerNewsService(IHackerNewsApiClient apiClient, IOptions<HackerNewsOptions> options)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<IReadOnlyList<BestStoryResponse>> GetBestStoriesAsync(int count, CancellationToken cancellationToken = default)
        {
            if (count < 1)
                count = 10;

            var storyIds = await _apiClient.GetBestStoryIdsAsync(cancellationToken);

            var stories = new System.Collections.Concurrent.ConcurrentBag<HackerNewsItem>();
            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = _options.Parallelism.MaxDegreeOfParallelism,
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

