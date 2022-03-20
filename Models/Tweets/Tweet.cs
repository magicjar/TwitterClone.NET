using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

[Serializable]
[Table("Tweets")]
public class Tweet
{
    public Guid Id { get; set; }
    [MaxLength(280)] public string Content { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long UserId { get; set; }
    public virtual ApplicationUser? User { get; set; }
}