using Microsoft.AspNetCore.Identity;

[Serializable]
public class ApplicationUser : IdentityUser<long>
{
    public long FollowerCount { get; set; }
    public long FollowingCount { get; set; }

    public virtual ICollection<Tweet>? Tweets { get; set; }
}
