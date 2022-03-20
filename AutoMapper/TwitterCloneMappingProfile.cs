using AutoMapper;

namespace TwitterClone.AutoMapper;

public class TwitterCloneMappingProfile : Profile
{
    public TwitterCloneMappingProfile()
    {
        CreateMap<CreateTweetDto, Tweet>();
        CreateMap<Tweet, TweetDto>();
    }
}
