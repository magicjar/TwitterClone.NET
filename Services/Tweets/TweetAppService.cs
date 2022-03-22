using TwitterClone.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Tweets.FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity == null) throw new NullReferenceException();
        _context.Tweets.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TweetDto> GetAsync(Guid id)
    {
        var entity = await _context.Tweets.FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity == null) throw new NullReferenceException();
        return _mapper.Map<Tweet, TweetDto>(entity);
    }

    public async Task<List<TweetDto>> GetListAsync(Expression<Func<Tweet, bool>> predicate)
    {
        var entities = await _context.Tweets.OrderByDescending(x => x.CreatedAt).Where(predicate).ToListAsync();
        return _mapper.Map<List<Tweet>, List<TweetDto>>(entities);
    }

    public async Task<TweetDto> UpdateAsync(Guid id, CreateTweetDto input)
    {
        var entity = await _context.Tweets.FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity == null) throw new NullReferenceException();

        var mappedEntity = _mapper.Map(input, entity);

        _context.Tweets.Attach(mappedEntity);

        var updatedEntity = _context.Tweets.Update(mappedEntity).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<Tweet, TweetDto>(updatedEntity);
    }
}
