using Microsoft.AspNetCore.Identity;

namespace Selu383.SP24.Api
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (userManager.Users.Any()) 
            {
                return;
            }

            await userManager.CreateAsync(new User
            {
                Email = "foo@foo.com",
                UserName = "foo"
            }, "Password123!");

        }
    }
}
