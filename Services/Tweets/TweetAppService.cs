using TwitterClone.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;

namespace TwitterClone.Services;

public class TweetAppService : ITweetAppService
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _context;

    public TweetAppService(
        ILogger<TweetAppService> logger,
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

    [Authorize]
    public async Task<TweetDto> CreateAsync(CreateTweetDto input)
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);

        Tweet newTweet = _mapper.Map<CreateTweetDto, Tweet>(input);
        newTweet.Id = Guid.NewGuid();
        newTweet.CreatedAt = DateTime.UtcNow;
        newTweet.UpdatedAt = newTweet.CreatedAt;
        newTweet.UserId = user.Id;

        try
        {
            await _context.Tweets.AddAsync(newTweet);
            await _context.SaveChangesAsync();
            newTweet.User = user;
            return _mapper.Map<Tweet, TweetDto>(newTweet);
        }
        catch (System.Exception)
        {
            throw;
        }
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
