using TwitterClone.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TwitterClone.Models;

namespace TwitterClone.Services;

public class FriendshipAppService : IFriendshipAppService
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public FriendshipAppService(
        ILogger<FriendshipAppService> logger,
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext context)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<FriendshipDto> CreateFollow(long friendId)
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var friend = await _context.Users.FirstOrDefaultAsync(e => e.Id.Equals(friendId));

        if (friend == null) throw new KeyNotFoundException();

        Friendship newFriend = new Friendship
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            FriendId = friendId,
            Status = FriendshipStatus.Friend,
            CreatedAt = DateTime.UtcNow,
        };

        try
        {
            await _context.Friendships.AddAsync(newFriend);
            await _context.SaveChangesAsync();
            newFriend.Friend = friend;
            return _mapper.Map<Friendship, FriendshipDto>(newFriend);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task DeleteFollow(long friendId)
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var friendship = await _context.Friendships.Where(x => x.UserId.Equals(user.Id)).Where(x => x.FriendId.Equals(friendId)).SingleAsync();

        if (friendship == null) throw new NullReferenceException();
        _context.Friendships.Remove(friendship);
        await _context.SaveChangesAsync();
    }

    public Task<List<FriendshipDto>> GetFollowerListAsync(Expression<Func<Tweet, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<List<FriendshipDto>> GetFollowingListAsync(Expression<Func<Tweet, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}