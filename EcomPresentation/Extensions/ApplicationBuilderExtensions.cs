using EcomDomain.Entity;
using EcomInfrastructure.DataContext;
using Microsoft.AspNetCore.Identity;

namespace EcomPresentation.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            await DataSeed.SeedRolesAsync(roleManager);
            await DataSeed.SeedUserAsync(userManager, roleManager);
        }
    }
}
