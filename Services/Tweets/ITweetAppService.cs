using System.Linq.Expressions;

namespace TwitterClone.Services;

public interface ITweetAppService
{
    Task<TweetDto> GetAsync(Guid id);
    Task<List<TweetDto>> GetListAsync(Expression<Func<Tweet, bool>> predicate);
    Task<TweetDto> CreateAsync(CreateTweetDto input);
    Task<TweetDto> UpdateAsync(Guid id, CreateTweetDto input);
    Task DeleteAsync(Guid id);
}
