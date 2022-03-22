
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Services;

namespace TwitterClone.Controllers;

[Authorize]
[Route("tweets")]
public class TweetController : Controller
{
    private readonly ITweetAppService _tweetService;

    public TweetController(ITweetAppService tweetService)
    {
        _tweetService = tweetService;
    }

    [HttpPost("create")]
    public async Task<TweetDto> Create(CreateTweetDto input)
    {
        return await _tweetService.CreateAsync(input);
    }

    [HttpDelete("delete")]
    public async Task Delete(Guid id)
    {
        await _tweetService.DeleteAsync(id);
    }
}
