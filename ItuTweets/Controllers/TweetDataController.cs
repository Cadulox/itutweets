using ItuTweets.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ItuTweets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetDataController : ControllerBase
    {
        private readonly IScrapperService _scrapperService;
        private readonly ITweetDataService _tweetDataService;

        public TweetDataController(IScrapperService scrapperService, ITweetDataService tweetDataService)
        {
            _scrapperService = scrapperService;
            _tweetDataService = tweetDataService;
        }

        [HttpGet("/feed_database")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public IActionResult FeedDatabase()
        {
            try
            {
                _scrapperService.FillDatabase();
                return Ok("Filled Database");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("/top_five_users")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetTopFiveUsers()
        {
            var users = _tweetDataService.GetTopFiveUsers();
            if (users == null)
                return NotFound("No user record found!");

            return Ok(users);
        }

        [HttpGet("/posts_by_hour")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public ActionResult<string> GetPostsByHour()
        {
            return Ok();
        }

        [HttpGet("/posts_by_tag")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetPostsGroupByLanguage(string tag)
        {
            var result = _tweetDataService.GetPostsByTag(tag);
            if (result == null || !result.Any())
                return NotFound("No record found!");

            return Ok(result);
        }


    }
}
