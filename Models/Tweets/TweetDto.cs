using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

[Serializable]
public class TweetDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
}

[Serializable]
public class CreateTweetDto
{
    public string Content { get; set; } = "";
}