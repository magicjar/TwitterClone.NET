using TwitterClone.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TwitterClone.Services;

public class TweetAppService : ITweetAppService
{
    private readonly ILogger _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public TweetAppService(
        ILogger<TweetAppService> logger,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    [Authorize]
    public async Task<TweetDto> CreateAsync(CreateTweetDto input)
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        Tweet newTweet = new Tweet()
        {
            Id = Guid.NewGuid(),
            Content = input.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            UserId = user.Id
        };
        var newSavedTweet = await _context.Tweets.AddAsync(newTweet);
        await _context.SaveChangesAsync();

        return new TweetDto()
        {
            Id = newSavedTweet.Entity.Id,
            Content = newSavedTweet.Entity.Content,
            CreatedAt = newSavedTweet.Entity.CreatedAt,
            UpdatedAt = newSavedTweet.Entity.UpdatedAt,
            UserId = newSavedTweet.Entity.UserId,
            User = user
        };
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<TweetDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TweetDto>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TweetDto> UpdateAsync(Guid id, CreateTweetDto input)
    {
        throw new NotImplementedException();
    }
}
