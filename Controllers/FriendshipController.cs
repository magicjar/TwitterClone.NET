
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Services;

namespace TwitterClone.Controllers;

[Route("friendships")]
public class FriendshipController : Controller
{
    private readonly IFriendshipAppService _friendshipService;

    public FriendshipController(IFriendshipAppService friendshipService)
    {
        _friendshipService = friendshipService;
    }

    [Authorize]
    [HttpPost("follow")]
    public async Task CreateFollow(long friendId)
    {
        await _friendshipService.CreateFollow(friendId);
    }

    [Authorize]
    [HttpDelete("unfollow")]
    public async Task DeleteFollow(long friendId)
    {
        await _friendshipService.DeleteFollow(friendId);
    }
}
