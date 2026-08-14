using Microsoft.AspNetCore.Mvc;

using Santander.DevCodingTest.Models;
using Santander.DevCodingTest.Services;

namespace Santander.DevCodingTest.Controllers
{
    [ApiController]
    [Route("api/hacker-news/stories")]
    public class HackerNewsTopStoriesController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        public HackerNewsTopStoriesController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("best")]
        [ProducesResponseType(typeof(IEnumerable<BestStoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBestStories([FromQuery] int top, CancellationToken cancellationToken)
        {

            var stories = await _hackerNewsService.GetBestStoriesAsync(top, cancellationToken);

            return Ok(stories);
        }
    }
}
