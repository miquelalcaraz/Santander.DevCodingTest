namespace Santander.DevCodingTest
{
    public class HackerNewsOptions
    {
        public const string SectionName = "HackerNews";

        public string BaseUrl { get; set; } = "https://hacker-news.firebaseio.com/";
        public TimeSpan StoryIdsTtl { get; set; } = TimeSpan.FromMinutes(5);
        public TimeSpan ItemTtl { get; set; } = TimeSpan.FromMinutes(5);
        public ParallelismOptions Parallelism { get; set; } = new();
        public RateLimitingOptions RateLimiting { get; set; } = new();

        public class ParallelismOptions
        {
            public int MaxDegreeOfParallelism { get; set; } = 10;
        }

        public class RateLimitingOptions
        {
            public int PermitLimit { get; set; } = 3;
            public TimeSpan Window { get; set; } = TimeSpan.FromSeconds(60);
            public int QueueLimit { get; set; } = 0;
        }
    }

}
