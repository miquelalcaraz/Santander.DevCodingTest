using Santander.DevCodingTest.Models;

namespace Santander.DevCodingTest.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private const string BestStoriesPath = "v0/beststories.json";
        private const string ItemPath = "v0/item/{0}.json";
        private readonly HttpClient httpClient;


        public HackerNewsService(HttpClient httpClient)
        {
            ArgumentNullException.ThrowIfNull(httpClient);
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyList<BestStoryResponse>> GetBestStoriesAsync(int count, CancellationToken cancellationToken = default)
        {
            if (count < 1)
            {
                count = 10;
            }

            var storyIds = await httpClient.GetFromJsonAsync<int[]>(BestStoriesPath, cancellationToken)
                ?? Array.Empty<int>();


            var storyTasks = storyIds.Select(storyId => GetStoryAsync(storyId, cancellationToken));

            var stories = await Task.WhenAll(storyTasks);

            return stories
                .Where(story => story is not null)
                .Select(story => ToResponse(story!))
                .OrderByDescending(story => story.Score)
                .Take(count)
                .ToArray();
        }

        internal async Task<HackerNewsItem?> GetStoryAsync(int storyId, CancellationToken cancellationToken)
        {
            return await httpClient.GetFromJsonAsync<HackerNewsItem>(string.Format(ItemPath, storyId), cancellationToken);
        }

        private static BestStoryResponse ToResponse(HackerNewsItem story)
        {
            return new BestStoryResponse
            {
                Title = story.Title,
                Uri = story.Url,
                PostedBy = story.Author,
                Time = story.UnixTime.HasValue
                    ? DateTimeOffset.FromUnixTimeSeconds(story.UnixTime.Value)
                    : default,
                Score = story.Score ?? 0,
                CommentCount = story.Descendants ?? 0
            };
        }
    }

}

