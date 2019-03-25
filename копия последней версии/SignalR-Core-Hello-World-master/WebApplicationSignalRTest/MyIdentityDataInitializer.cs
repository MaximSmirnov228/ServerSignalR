using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ServerNetCore.Models;

namespace ServerNetCore
{
    public static class MyIdentityDataInitializer

    {
        private const string RoleUser = "User";

        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                (RoleUser).Result)
            {
                var role = new IdentityRole();
                role.Name = RoleUser;
                //role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            string Email = "admin@gmail.com";
            string Password = "Qwerty12345&";

            if (userManager.FindByNameAsync
                    (Email).Result == null)
            {
                User user = new User();
                user.UserName = Email;
                user.Email = Email;

                IdentityResult result = userManager.CreateAsync
                    (user, Password).Result;

                if (result.Succeeded)
                {
                    var resWait = userManager.AddToRoleAsync(user,
                        RoleUser).Result;
                }
            }
        }
    }
}