using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterClone.Data;

namespace TwitterClone.Models;

public static class UserDataContributor
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Users.Any()) return;

            string[] roles = new string[] { "Administrator", "Manager", "Editor", "User" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role));
                }
            }

            var user = new IdentityUser
            {
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                UserName = "admin",
                NormalizedUserName = "admin",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "admin");
                user.PasswordHash = hashed;

                var userStore = new UserStore<IdentityUser>(context);
                var result = userStore.CreateAsync(user);
            }

            await AssignRoles(serviceProvider, user.Email, roles);

            await context.SaveChangesAsync();
        }
    }

    public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
    {
        UserManager<IdentityUser> _userManager = services.GetService<UserManager<IdentityUser>>();
        IdentityUser user = await _userManager.FindByEmailAsync(email);
        var result = await _userManager.AddToRolesAsync(user, roles);

        return result;
    }
}
