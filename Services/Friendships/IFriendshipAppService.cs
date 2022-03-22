using System.Linq.Expressions;
using TwitterClone.Models;

namespace TwitterClone.Services;

public interface IFriendshipAppService
{
    /// <summary>
    /// Follow
    /// </summary>
    /// <returns></returns>
    Task<FriendshipDto> CreateFollow(long friendId);

    /// <summary>
    /// Unfollow
    /// </summary>
    /// <returns></returns>
    Task DeleteFollow(long friendId);

    /// <summary>
    /// Follower
    /// </summary>
    /// <returns></returns>
    Task<List<FriendshipDto>> GetFollowerListAsync();

    /// <summary>
    /// Following
    /// </summary>
    /// <returns></returns>
    Task<List<FriendshipDto>> GetFollowingListAsync();
}
