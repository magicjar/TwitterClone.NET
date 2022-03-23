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
        var friend = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(friendId));

        if (user == null) throw new KeyNotFoundException();
        if (friend == null) throw new KeyNotFoundException();

        var existing = await _context.Friendships.AsNoTracking().Where(x => x.UserId.Equals(user.Id)).Where(x => x.FriendId.Equals(friendId)).FirstOrDefaultAsync();
        if (existing != null) throw new FormatException("Already following.");

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
            newFriend.Friend = friend;

            await _context.SaveChangesAsync();

            user.FollowingCount = await GetFollowingCountAsync(user);
            friend.FollowerCount = await GetFollowerCountAsync(friend);

            _context.Users.Attach(user);
            var updatedCurrentUser = _context.Users.Update(user).Entity;

            _context.Users.Attach(friend);
            var updatedFriend = _context.Users.Update(friend).Entity;

            await _context.SaveChangesAsync();

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
        var friend = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(friendId));
        var friendship = await _context.Friendships.AsNoTracking().Where(x => x.UserId.Equals(user.Id)).Where(x => x.FriendId.Equals(friendId)).SingleAsync();

        if (user == null) throw new KeyNotFoundException();
        if (friend == null) throw new KeyNotFoundException();

        if (friendship == null) throw new NullReferenceException();
        _context.Friendships.Remove(friendship);

        await _context.SaveChangesAsync();

        user.FollowingCount = await GetFollowingCountAsync(user);
        friend.FollowerCount = await GetFollowerCountAsync(friend);

        _context.Users.Attach(user);
        var updatedCurrentUser = _context.Users.Update(user).Entity;

        _context.Users.Attach(friend);
        var updatedFriend = _context.Users.Update(friend).Entity;

        await _context.SaveChangesAsync();
    }

    public async Task<List<FriendshipDto>> GetFollowerListAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var followers = await _context.Friendships.AsNoTracking().Include(x => x.User).OrderByDescending(x => x.CreatedAt).Where(x => x.FriendId == user.Id).ToListAsync();
        return _mapper.Map<List<Friendship>, List<FriendshipDto>>(followers);
    }

    public async Task<List<FriendshipDto>> GetFollowingListAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var followers = await _context.Friendships.AsNoTracking().Include(x => x.Friend).OrderByDescending(x => x.CreatedAt).Where(x => x.UserId == user.Id).ToListAsync();
        return _mapper.Map<List<Friendship>, List<FriendshipDto>>(followers);
    }

    public async Task<long> GetFollowerCountAsync(ApplicationUser user)
    {
        return await _context.Friendships.AsNoTracking().Include(x => x.User).OrderByDescending(x => x.CreatedAt).Where(x => x.FriendId == user.Id).LongCountAsync();
    }

    public async Task<long> GetFollowingCountAsync(ApplicationUser user)
    {
        return await _context.Friendships.AsNoTracking().Include(x => x.Friend).OrderByDescending(x => x.CreatedAt).Where(x => x.UserId == user.Id).LongCountAsync();
    }
}
