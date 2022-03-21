using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Models;

[Serializable]
[Table("Friendships")]
public class Friendship
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public long FriendId { get; set; }
    public FriendshipStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ApplicationUser? User { get; set; }
    public virtual ApplicationUser? Friend { get; set; }
}