#region Usings

using Bogus;
using Microsoft.AspNetCore.Identity;
using Moviebase.DAL.Migrations;
using Moviebase.DAL.Model.Identity;

#endregion

namespace Moviebase.DAL;

public static class Seed
{
    public static async Task SeedUsersAsync(
        UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        await roleManager.CreateAsync(new Role{ Name = "User" });
        await roleManager.CreateAsync(new Role { Name = "Admin" });

        var adminUser = new User { UserName = "admin" };

        await userManager.CreateAsync(adminUser, "admin");
        await userManager.AddToRoleAsync(adminUser, "Admin");
        await userManager.AddToRoleAsync(adminUser, "User");

        var studentsFaker = new Faker<User>()
            .RuleFor(user => user.UserName, x => x.Name.FirstName())
            .RuleFor(user => user.Created, x => x.Date.Past(2));

        foreach (var user in studentsFaker.GenerateLazy(1))
        {
            await userManager.CreateAsync(user, user.UserName ?? "Password");
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}
