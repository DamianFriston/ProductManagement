using Microsoft.EntityFrameworkCore;
using ProductManagement.Server.Model;

namespace ProductManagement.Server.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ProductContext(DbContextOptions options) : base(options)
        {

        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var fixedDate = new DateTime(2024, 11, 20, 12, 0, 0);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductName)
                    .HasMaxLength(100);

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(10);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)");


                entity.Property(e => e.ProductName)
                    .IsRequired();

                entity.Property(e => e.ProductCode)
                    .IsRequired();

                entity.HasOne(e => e.Category)
                    .WithMany()
                    .IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsRequired();
            });


            // Seeding Data

            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, CategoryName = "Food" },
               new Category { Id = 2, CategoryName = "Clothing" },
               new Category { Id = 3, CategoryName = "Electronics" },
               new Category { Id = 4, CategoryName = "Outdoor" },          
               new Category { Id = 5, CategoryName = "Books" },             
               new Category { Id = 6, CategoryName = "Health" },        
               new Category { Id = 7, CategoryName = "Furniture" }
              );

            modelBuilder.Entity<Product>().HasData(
              new Product { Id = 1, ProductName = "Bread", CategoryId = 1, StockQty = 15, Price = 3.00m, DateAdded = fixedDate, ProductCode = "SKU0010" },
              new Product { Id = 2, ProductName = "Ham", CategoryId = 1, StockQty = 18, Price = 4.00m, DateAdded = fixedDate, ProductCode = "SKU0011" },
              new Product { Id = 3, ProductName = "Cheese", CategoryId = 1, StockQty = 17, Price = 5.00m, DateAdded = fixedDate, ProductCode = "SKU0012" },

              new Product { Id = 4, ProductName = "Joggers", CategoryId = 2, StockQty = 12, Price = 30.50m, DateAdded = fixedDate, ProductCode = "SKU0013" },
              new Product { Id = 5, ProductName = "T-Shirt", CategoryId = 2, StockQty = 4, Price = 20.99m, DateAdded = fixedDate, ProductCode = "SKU0014" },
              new Product { Id = 6, ProductName = "Coat", CategoryId = 2, StockQty = 11, Price = 46.75m, DateAdded = fixedDate, ProductCode = "SKU0015" },

              new Product { Id = 7, ProductName = "Iron", CategoryId = 3, StockQty = 12, Price = 15.00m, DateAdded = fixedDate, ProductCode = "SKU0016" },
              new Product { Id = 8, ProductName = "Microwave", CategoryId = 3, StockQty = 18, Price = 30.15m, DateAdded = fixedDate, ProductCode = "SKU0017" },
              new Product { Id = 9, ProductName = "Hoover", CategoryId = 3, StockQty = 14, Price = 58.00m, DateAdded = fixedDate, ProductCode = "SKU0018" },

              new Product { Id = 10, ProductName = "Football", CategoryId = 4, StockQty = 101, Price = 5.00m, DateAdded = fixedDate , ProductCode = "SKU0019" },
              new Product { Id = 11, ProductName = "Tent", CategoryId = 4, StockQty = 64, Price = 30.00m, DateAdded = fixedDate, ProductCode = "SKU0020" },
              new Product { Id = 12, ProductName = "Surfboard", CategoryId = 4, StockQty = 23, Price = 144.95m, DateAdded = fixedDate, ProductCode = "SKU0021" },

              new Product { Id = 13, ProductName = "Harry Potter", CategoryId = 5, StockQty = 55, Price = 10.00m, DateAdded = fixedDate, ProductCode = "SKU0022" },
              new Product { Id = 14, ProductName = "Alan Watts", CategoryId = 5, StockQty = 12, Price = 12.80m, DateAdded = fixedDate, ProductCode = "SKU0023" },
              new Product { Id = 15, ProductName = "Fundamental Physics", CategoryId = 5, StockQty = 17, Price = 8.33m, DateAdded = fixedDate, ProductCode = "SKU0024" },

              new Product { Id = 16, ProductName = "Vitamin C", CategoryId = 6, StockQty = 42, Price = 2.00m, DateAdded = fixedDate, ProductCode = "SKU0025" },
              new Product { Id = 17, ProductName = "Ibuprofen", CategoryId = 6, StockQty = 115 , Price = 5.00m, DateAdded = fixedDate, ProductCode = "SKU0026" },
              new Product { Id = 18, ProductName = "Sunscreen SPF 30", CategoryId = 6, StockQty = 53, Price = 10.65m, DateAdded = fixedDate, ProductCode = "SKU0027" },

              new Product { Id = 19, ProductName = "Chair", CategoryId = 7, StockQty = 28, Price = 42.00m, DateAdded = fixedDate, ProductCode = "SKU0028" },
              new Product { Id = 20, ProductName = "Bed", CategoryId = 7, StockQty = 4, Price = 110.80m, DateAdded = fixedDate, ProductCode = "SKU0029" },
              new Product { Id = 21, ProductName = "Shelf", CategoryId = 7, StockQty = 9, Price = 75.00m, DateAdded = fixedDate, ProductCode = "SKU0030" }
             );
        }
    }
}
