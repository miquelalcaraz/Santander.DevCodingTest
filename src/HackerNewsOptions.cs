namespace Santander.DevCodingTest
{
    public class HackerNewsOptions
    {
        public const string SectionName = "HackerNews";

        // External API
        public string BaseUrl { get; set; } = "https://hacker-news.firebaseio.com/";

        // Cache
        public TimeSpan StoryIdsTtl { get; set; } = TimeSpan.FromMinutes(5);
        public TimeSpan ItemTtl { get; set; } = TimeSpan.FromMinutes(5);
        public int MaxParallelism { get; set; } = 10;

        // Rate limiting
        public int PermitLimit { get; set; } = 10;
        public int WindowSeconds { get; set; } = 10;
        public int QueueLimit { get; set; } = 2;
    }
}
