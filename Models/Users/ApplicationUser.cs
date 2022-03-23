using Microsoft.AspNetCore.Identity;

[Serializable]
public class ApplicationUser : IdentityUser<long>
{
    public long FollowingCount { get; set; }
    public long FollowerCount { get; set; }

    public virtual ICollection<Tweet>? Tweets { get; set; }
}
