using Microsoft.Extensions.Caching.Memory;

using Santander.DevCodingTest.Models;

namespace Santander.DevCodingTest.Services
{
    public class HackerNewsApiClient : IHackerNewsApiClient
    {
        private const string BestStoriesPath = "v0/beststories.json";
        private const string ItemPath = "v0/item/{0}.json";
        private readonly HttpClient _httpClient;
        private const string BestStoryIdsCacheKey = "hackernews:beststoryids";
        private readonly IMemoryCache _cache;


        public HackerNewsApiClient(HttpClient httpClient, IMemoryCache cache)

        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<IEnumerable<int>> GetBestStoryIdsAsync(CancellationToken cancellationToken = default)
        {
            if (_cache.TryGetValue(BestStoryIdsCacheKey, out int[]? cached))
                return cached!;

            var result = await _httpClient.GetFromJsonAsync<int[]>(BestStoriesPath, cancellationToken);
            if (result is not null)
                _cache.Set(BestStoryIdsCacheKey, result, TimeSpan.FromMinutes(5));

            return result ?? Array.Empty<int>();
        }

        public async Task<HackerNewsItem?> GetItemAsync(int id, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"hackernews:item:{id}";

            if (_cache.TryGetValue(cacheKey, out HackerNewsItem? cached))
                return cached;

            var item = await _httpClient.GetFromJsonAsync<HackerNewsItem>(string.Format(ItemPath, id), cancellationToken);

            if (item is not null)
                _cache.Set(cacheKey, item, TimeSpan.FromMinutes(5));

            return item;
        }
    }
}
