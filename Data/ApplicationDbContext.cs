using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterClone.Models;

namespace TwitterClone.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tweet> Tweets { get; set; }
    public DbSet<Friendship> Friendships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Friendship>(us =>
        {
            us.HasOne(fs => fs.User)
              .WithMany()
              .HasForeignKey(fs => fs.UserId);

            us.HasOne(pt => pt.Friend)
              .WithMany()
              .HasForeignKey(pt => pt.FriendId);

            us.HasIndex(t => new { t.CreatedAt, t.Status });
        });

        modelBuilder.Entity<Tweet>(tw =>
        {
            tw.HasOne(t => t.User)
              .WithMany(u => u.Tweets)
              .HasForeignKey(t => t.UserId)
              .IsRequired();

            tw.HasIndex(t => t.CreatedAt);
        });
    }
}
