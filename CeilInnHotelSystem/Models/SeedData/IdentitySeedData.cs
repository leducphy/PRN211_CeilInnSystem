using CeilInnHotelSystem.Models;
using CeilInnHotelSystem.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CeilInnHotelSystem.Model.SeedData
{
    public static class IdentitySeedData
    {

        public static async Task Seed(CeilInnHotelDbContext context, UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {

            context.Database.Migrate(); //?
            if(await roleManager.FindByNameAsync(RoleConstant.ADMIN) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(RoleConstant.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(RoleConstant.EMPLOYEE));
                await roleManager.CreateAsync(new IdentityRole(RoleConstant.CUSTOMER));
            }
            if(await userManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                var user = new AppUser
                {
                    Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                    FullName = "Administrator",
                    Login = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Type = 2,
                    Activated = true,
                    LockoutEnabled = false,
                    PhoneNumber = "1234567890"
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "Abc@123");
                    await userManager.AddToRoleAsync(user, RoleConstant.ADMIN);
                }
            }
            
        }
    }
}
