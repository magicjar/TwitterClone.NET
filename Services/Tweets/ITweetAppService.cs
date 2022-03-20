namespace TwitterClone.Services;

public interface ITweetAppService
{
    Task<TweetDto> GetAsync(Guid id);
    Task<List<TweetDto>> GetListAsync();
    Task<TweetDto> CreateAsync(CreateTweetDto input);
    Task<TweetDto> UpdateAsync(Guid id, CreateTweetDto input);
    Task DeleteAsync(Guid id);
}
