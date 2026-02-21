using EcomDomain.Entity;
using EcomDomain.Enum;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace EcomInfrastructure.DataContext
{
    public static class DataSeed
    {
        
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[]
            {
            new Role
            {
                Name = UserRolesEnum.Customer.ToString(),
                NormalizedName = UserRolesEnum.Customer.ToString().ToUpper(),
                AllowedForFoundation = false
            }
        };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
        public static async Task SeedUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string Email = "johndoe@yopmail.com";
            const string Password = "Doe#123456";

            var existingUser = await userManager.FindByEmailAsync(Email);

            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = Email,
                    Email = Email,
                    EmailConfirmed = true,
                    FirstName = "John",
                    LastName = "Doe",
                    PasswordHash = Password
                };
                var result = await userManager.CreateAsync(user, Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRolesEnum.Customer.ToString());
                }
                else
                {
                    throw new Exception("Failed to create customer user");
                }
            }
        }
        public static async Task SeedProduct(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<EcomDbContext>();
            var prodVariation = await context.ProductVariations.ToListAsync();
            if (prodVariation == null)
            {
                var baseDir = Directory.GetCurrentDirectory();
                var productPath = File.ReadAllText(FilePath(baseDir, "C:\\Users\\hp\\source\\repos\\EcomPresentation\\EcomInfrastructure\\JsonFile\\Products.json"));
                var productVariationPath = File.ReadAllText(FilePath(baseDir, "C:\\Users\\hp\\source\\repos\\EcomPresentation\\EcomInfrastructure\\JsonFile\\ProductVariations.json"));
                var products = JsonConvert.DeserializeObject<List<Product>>(productPath);
                var productvariations = JsonConvert.DeserializeObject<List<ProductVariation>>(productVariationPath);
                foreach (var item in products)
                {
                    var product = new Product()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        InStock = item.InStock,
                        TotalQuantity = item.TotalQuantity
                    };

                    await context.Products.AddAsync(product);
                }
                foreach (var item in productvariations)
                {
                    var variation = new ProductVariation()
                    {
                        Id = item.Id,
                        Color = item.Color,
                        Size = item.Size,
                        Quantity = item.Quantity,
                        ProductId = item.ProductId
                    };
                    await context.ProductVariations.AddAsync(variation);
                }
                await context.SaveChangesAsync();
            }
        }
        static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }
    }
}

