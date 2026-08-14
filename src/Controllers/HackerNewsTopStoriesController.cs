using Microsoft.AspNetCore.Mvc;

namespace Santander.DevCodingTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsTopStoriesController : Controller
    {
        [HttpGet(Name = "HackerNewsTopStories")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
