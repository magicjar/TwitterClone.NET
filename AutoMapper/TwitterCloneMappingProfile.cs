using AutoMapper;
using TwitterClone.Models;

namespace TwitterClone.AutoMapper;

public class TwitterCloneMappingProfile : Profile
{
    public TwitterCloneMappingProfile()
    {
        CreateMap<CreateTweetDto, Tweet>();
        CreateMap<Tweet, TweetDto>();
        CreateMap<CreateFriendshipDto, Friendship>();
        CreateMap<Friendship, FriendshipDto>();
    }
}
