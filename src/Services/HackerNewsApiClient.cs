using Santander.DevCodingTest.Models;

namespace Santander.DevCodingTest.Services
{
    public class HackerNewsApiClient : IHackerNewsApiClient
    {
        private const string BestStoriesPath = "v0/beststories.json";
        private const string ItemPath = "v0/item/{0}.json";

        private readonly HttpClient _httpClient;

        public HackerNewsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<int>> GetBestStoryIdsAsync(CancellationToken cancellationToken = default)
        {
            var result = await _httpClient.GetFromJsonAsync<int[]>(BestStoriesPath, cancellationToken);
            return result ?? Array.Empty<int>(); ;
        }

        public async Task<HackerNewsItem?> GetItemAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _httpClient.GetFromJsonAsync<HackerNewsItem>(string.Format(ItemPath, id), cancellationToken);
            return result;
        }
    }
}
