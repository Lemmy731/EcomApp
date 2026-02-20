using EcomDomain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EcomInfrastructure.DataContext
{
    public class EcomDbContext : IdentityDbContext
    {
        public EcomDbContext(DbContextOptions<EcomDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Cart>()
           .HasOne(c => c.Product)
           .WithMany(p => p.Carts)
           .HasForeignKey(c => c.ProductId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Cart>()
           .HasOne(c => c.User)
           .WithMany(u => u.Carts)
           .HasForeignKey(c => c.UserId) 
           .OnDelete(DeleteBehavior.Cascade);  
            
            builder.Entity<ProductVariation>()
           .HasOne(v => v.Product)
           .WithMany(p => p.Variations)
           .HasForeignKey(v => v.ProductId)
           .OnDelete(DeleteBehavior.Cascade);   
        }
    }
}
