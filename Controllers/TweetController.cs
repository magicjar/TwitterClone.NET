
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Services;

namespace TwitterClone.Controllers;

[Route("tweets")]
public class TweetController : Controller
{
    private readonly ITweetAppService _tweetService;

    public TweetController(ITweetAppService tweetService)
    {
        _tweetService = tweetService;
    }

    [HttpPost("create")]
    public async Task Create(CreateTweetDto input)
    {
        await _tweetService.CreateAsync(input);
    }
}
