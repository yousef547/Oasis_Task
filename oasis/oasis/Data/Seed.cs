using oasis.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace oasis.Data
{
    public class Seed
    {

        public static async Task UserSeed(UserManager<AppUser> userManger,RoleManager<AppRole> roleManager)
        {
            if (await userManger.Users.AnyAsync()) return;
            var roles = new List<AppRole>
            {
                new AppRole{ Name = "Member"},
                new AppRole{ Name = "Admin"},
                new AppRole{ Name = "Moderator"},
            };

            foreach(var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

        

            var admin1 = new AppUser
            {
                UserName = "admin1"
            };
            await userManger.CreateAsync(admin1, "Ad123456");
            await userManger.AddToRolesAsync(admin1, new[] { "Admin" });


            var admin2 = new AppUser
            {
                UserName = "admin2"
            };
            await userManger.CreateAsync(admin2, "Ad123456");
            await userManger.AddToRolesAsync(admin2, new[] { "Moderator" });

            var admin3 = new AppUser
            {
                UserName = "admin3"
            };
            await userManger.CreateAsync(admin3, "Ad123456");
            await userManger.AddToRolesAsync(admin3, new[] { "Member" });

            var admin4 = new AppUser
            {
                UserName = "admin4"
            };
            await userManger.CreateAsync(admin4, "Ad123456");
            await userManger.AddToRolesAsync(admin4, new[] { "Member", "Moderator", "Admin" });

        }
    }
}
