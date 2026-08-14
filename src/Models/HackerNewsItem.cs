using System.Text.Json.Serialization;

namespace Santander.DevCodingTest.Models
{
    public class HackerNewsItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("by")]
        public string? Author { get; set; }

        [JsonPropertyName("time")]
        public long? UnixTime { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("score")]
        public int? Score { get; set; }

        [JsonPropertyName("descendants")]
        public int? Descendants { get; set; }
    }
}
