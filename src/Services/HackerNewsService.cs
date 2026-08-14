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

            var storyTasks = storyIds.Select(id => _apiClient.GetItemAsync(id, cancellationToken));
            var stories = await Task.WhenAll(storyTasks);

            return stories
                .Where(story => story is not null)
               .Select(story => BestStoryResponse.From(story!))
                .OrderByDescending(story => story.Score)
                .Take(count)
                .ToArray();
        }

    }

}

