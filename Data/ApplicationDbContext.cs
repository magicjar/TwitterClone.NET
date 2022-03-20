using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TwitterClone.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
