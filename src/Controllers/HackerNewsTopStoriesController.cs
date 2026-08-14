using Microsoft.AspNetCore.Mvc;

using Santander.DevCodingTest.Models;

namespace Santander.DevCodingTest.Controllers
{
    [ApiController]
    [Route("api/hacker-news/stories")]
    public class HackerNewsTopStoriesController : ControllerBase
    {
        [HttpGet("best")]
        public IActionResult Get([FromQuery] int top)
        {
            var bestStoryResponse = new List<BestStoryResponse>
        {
            new BestStoryResponse
            {
                Title = "Sample Story",
                Uri = "https://example.com/sample-story",
                PostedBy = "sampleuser",
                Time = DateTimeOffset.UtcNow,
                Score = 100,
                CommentCount = 50
            }
        };

            return Ok(bestStoryResponse);
        }
    }
}
