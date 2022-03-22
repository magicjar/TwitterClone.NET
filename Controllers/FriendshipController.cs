
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Models;
using TwitterClone.Services;

namespace TwitterClone.Controllers;

[Authorize]
[Route("friendships")]
public class FriendshipController : Controller
{
    private readonly IFriendshipAppService _friendshipService;

    public FriendshipController(IFriendshipAppService friendshipService)
    {
        _friendshipService = friendshipService;
    }

    [HttpPost("follow")]
    public async Task CreateFollow(long friendId)
    {
        await _friendshipService.CreateFollow(friendId);
    }

    [HttpDelete("unfollow")]
    public async Task DeleteFollow(long friendId)
    {
        await _friendshipService.DeleteFollow(friendId);
    }

    [HttpGet("follower")]
    public async Task<List<FriendshipDto>> GetFollowerList()
    {
        return await _friendshipService.GetFollowerListAsync();
    }

    [HttpGet("following")]
    public async Task<List<FriendshipDto>> GetFollowingList()
    {
        return await _friendshipService.GetFollowingListAsync();
    }
}
