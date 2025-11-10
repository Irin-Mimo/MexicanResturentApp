using MexicanResturent.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MexicanResturent.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ingrediant> Ingrediants { get; set; }
        public DbSet<ProductIngrediant> ProductIngrediants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductIngrediant>()
                .HasKey(pi => new { pi.ProductId, pi.IngrediantId });

            modelBuilder.Entity<ProductIngrediant>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngrediants)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductIngrediant>()
                .HasOne(pi => pi.Ingrediant)
                .WithMany(i => i.ProductIngrediants)
                .HasForeignKey(pi => pi.IngrediantId);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Appetizer" },
                new Category { CategoryId = 2, Name = "Entree" },
                new Category { CategoryId = 3, Name = "Side Dish" }
            );

            modelBuilder.Entity<Ingrediant>().HasData(
                new Ingrediant { IngrediantId=1, Name="Beef" },
                new Ingrediant { IngrediantId=2, Name="Chicken" },
                new Ingrediant { IngrediantId=3, Name="Fish" },
                new Ingrediant { IngrediantId=4, Name="Tortilla" },
                new Ingrediant { IngrediantId=5, Name="Lettuce" },
                new Ingrediant { IngrediantId=6, Name="Tomato" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    ProductId = 1, 
                    Name = "Beef Taco", 
                    Description = "Delicious beef taco with fresh ingredients", 
                    Price = 3.99m, 
                    Stock = 100, 
                    CategoryId = 1 },

                new Product { 
                    ProductId = 2, 
                    Name = "Chicken Burrito", 
                    Description = "Tasty chicken burrito with rice and beans", 
                    Price = 5.99m, 
                    Stock = 80, 
                    CategoryId = 2 },

                new Product { 
                    ProductId = 3, 
                    Name = "Fish Quesadilla", 
                    Description = "Crispy fish quesadilla with melted cheese", 
                    Price = 4.99m, 
                    Stock = 50, 
                    CategoryId = 3 
                }
            );

            modelBuilder.Entity<ProductIngrediant>().HasData(
                new ProductIngrediant { ProductId = 1, IngrediantId=1 },
                new ProductIngrediant { ProductId = 1, IngrediantId=4 },
                new ProductIngrediant { ProductId = 1, IngrediantId=5 },
                new ProductIngrediant { ProductId = 1, IngrediantId=6 },

                new ProductIngrediant { ProductId = 2, IngrediantId=2 },
                new ProductIngrediant { ProductId = 2, IngrediantId=4 },
                new ProductIngrediant { ProductId = 2, IngrediantId=5 },
                new ProductIngrediant { ProductId = 2, IngrediantId=6 },

                new ProductIngrediant { ProductId = 3, IngrediantId=3 },
                new ProductIngrediant { ProductId = 3, IngrediantId=4 },
                new ProductIngrediant { ProductId = 3, IngrediantId=5 },
                new ProductIngrediant { ProductId = 3, IngrediantId=6 }
                );
        }
    }
}
