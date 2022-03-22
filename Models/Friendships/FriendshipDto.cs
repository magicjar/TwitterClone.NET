using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TwitterClone.Models;

[Serializable]
public class FriendshipDto
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public long FriendId { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ApplicationUser? Friend { get; set; }
}

[Serializable]
public class CreateFriendshipDto
{
    public long UserId { get; set; }
    public long FriendId { get; set; }
    public DateTime CreatedAt { get; set; }
}
