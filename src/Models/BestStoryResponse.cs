namespace Santander.DevCodingTest.Models
{
    public class BestStoryResponse
    {
        public string? Title { get; set; }

        public string? Uri { get; set; }

        public string? PostedBy { get; set; }

        public DateTimeOffset Time { get; set; }

        public int Score { get; set; }

        public int CommentCount { get; set; }

        public static BestStoryResponse From(HackerNewsItem story)
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
