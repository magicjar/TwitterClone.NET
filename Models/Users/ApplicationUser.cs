using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<long>
{
    public long FollowerCount { get; set; }
    public long FollowingCount { get; set; }
}
