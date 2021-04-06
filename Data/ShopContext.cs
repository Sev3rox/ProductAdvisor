using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<webapp.Models.Product> Product { get; set; }
        public DbSet<webapp.Models.Category> Category { get; set; }
        public DbSet<webapp.Models.Account> Accounts { get; set; }
        public DbSet<webapp.Models.Company> Company { get; set; }
        public DbSet<webapp.Models.Forum> Forum { get; set; }
        public DbSet<webapp.Models.Comment> Commment { get; set; }
        public DbSet<webapp.Models.Product_Category> Product_Category { get; set; }
        public DbSet<webapp.Models.Blocked> Blocked { get; set; }
        public DbSet<webapp.Models.Ulubione> Ulubione { get; set; }
        public DbSet<webapp.Models.ReviewProduct> ReviewProduct { get; set; }
        public DbSet<webapp.Models.CommentReview> CommentReview { get; set; }
        public DbSet<webapp.Models.ProductDescription> ProductDescription { get; set; }
        public DbSet<webapp.Models.Glosowanie> Głosowanie { get; set; }
        public DbSet<webapp.Models.ProductGlosowanie> ProductGlosowanie { get; set; }
        public DbSet<webapp.Models.AccountGlosowanie> AccountGlosowanies { get; set; }
        public DbSet<webapp.Models.Decorations> Decorations { get; set; }
        public DbSet<webapp.Models.Account_Decorations> Account_Decorations { get; set; }
        public DbSet<webapp.Models.Account_Review> Account_Review { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Forum>().ToTable("Forum");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Product_Category>().HasKey(i => new { i.ProductId, i.CategoryId });
            modelBuilder.Entity<Blocked>().HasKey(i => new { i.AccountId, i.CompanyId });
            modelBuilder.Entity<Ulubione>().HasKey(i => new { i.AccountId, i.ProductId });
            modelBuilder.Entity<ReviewProduct>().ToTable("ReviewProduct");
            modelBuilder.Entity<CommentReview>().ToTable("CommentReview");
            modelBuilder.Entity<ProductDescription>().ToTable("ProductDescription");
            modelBuilder.Entity<Glosowanie>().ToTable("Głosowanie");
            modelBuilder.Entity<ProductGlosowanie>().HasKey(i => new { i.GlosowanieId, i.ProductId });
            modelBuilder.Entity<AccountGlosowanie>().HasKey(i => new { i.GlosowanieId, i.AccountId });
            modelBuilder.Entity<Decorations>().ToTable("Decorations");
            modelBuilder.Entity<Account_Decorations>().HasKey(i => new { i.AccountId, i.DecorationsId });
            modelBuilder.Entity<Account_Review>().HasKey(i => new { i.AccountId, i.ProductId });

            modelBuilder.Entity<Decorations>().HasData(new Decorations { Id = 1, Name = "10Forum", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 2, Name = "100Forum", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 3, Name = "1000Forum", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 4, Name = "10Komentarze", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 5, Name = "100Komentarze", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 6, Name = "1000Komentarze", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 7, Name = "10Oceny", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 8, Name = "100Oceny", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 9, Name = "1000Oceny", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 10, Name = "10Recenzje", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 11, Name = "100Recenzje", Image = File.ReadAllBytes("wwwroot/photos/noone.png") },
                new Decorations { Id = 12, Name = "1000Recenzje", Image = File.ReadAllBytes("wwwroot/photos/noone.png") }
            );


            modelBuilder.Entity<Account>().HasData(new Account { Id = 1, Username = "user1", Password = BCrypt.Net.BCrypt.HashPassword("pass1"), Email = "user1@mail", role = 3 },
                new Account { Id = 2, Username = "user2", Password = BCrypt.Net.BCrypt.HashPassword("pass2"), Email = "user2@mail", role = 0 },
                new Account { Id = 3, Username = "user3", Password = BCrypt.Net.BCrypt.HashPassword("pass3"), Email = "user3@mail", role = 0 },
                new Account { Id = 4, Username = "user4", Password = BCrypt.Net.BCrypt.HashPassword("pass4"), Email = "user4@mail", role = 0 },
                new Account { Id = 5, Username = "user5", Password = BCrypt.Net.BCrypt.HashPassword("pass5"), Email = "user5@mail", role = 0 },
                new Account { Id = 6, Username = "user6", Password = BCrypt.Net.BCrypt.HashPassword("pass6"), Email = "user6@mail", role = 0 },
                new Account { Id = 7, Username = "user7", Password = BCrypt.Net.BCrypt.HashPassword("pass7"), Email = "user7@mail", role = 0 },
                new Account { Id = 8, Username = "user8", Password = BCrypt.Net.BCrypt.HashPassword("pass8"), Email = "user8@mail", role = 0 },
                new Account { Id = 9, Username = "user9", Password = BCrypt.Net.BCrypt.HashPassword("pass9"), Email = "user9@mail", role = 0 },
                new Account { Id = 10, Username = "user10", Password = BCrypt.Net.BCrypt.HashPassword("pass10"), Email = "user10@mail", role = 0 },
                new Account { Id = 11, Username = "user11", Password = BCrypt.Net.BCrypt.HashPassword("pass11"), Email = "user11@mail", role = 0 },
                new Account { Id = 12, Username = "user12", Password = BCrypt.Net.BCrypt.HashPassword("pass12"), Email = "user12@mail", role = 0 },
                new Account { Id = 13, Username = "user13", Password = BCrypt.Net.BCrypt.HashPassword("pass13"), Email = "user13@mail", role = 0 }
           );

            var pom1 = new Company { ID = 1, Name = "Firma1", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Description = "firm1", Date = DateTime.Now, Location = "Białystok" };
            var pom2 = new Company { ID = 2, Name = "Firma2", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Description = "firm2", Date = DateTime.Now, Location = "Warszawa" };
            var pom3 = new Company { ID = 3, Name = "Firma3", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Description = "firm3", Date = DateTime.Now, Location = "Kraków" };

            modelBuilder.Entity<Company>().HasData(pom1,
                pom2,
                pom3
           );


            modelBuilder.Entity<Product>().HasData(new Product { Id = 1, Name = "prod1", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price = 1, Description = "to jest prod1", CompanyID = pom1.ID },
            new Product { Id = 2, Name = "prod2", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price = 2, Description = "to jest prod2", CompanyID = pom1.ID },
             new Product {Id=3, Name="prod3", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=3, Description="to jest prod3", CompanyID= pom1.ID },
             new Product {Id=4, Name="prod4", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=4, Description="to jest prod4", CompanyID= pom1.ID },
             new Product {Id=5, Name="prod5", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=5, Description="to jest prod5", CompanyID= pom1.ID },
             new Product {Id=6, Name="prod6", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=6, Description="to jest prod6", CompanyID= pom1.ID },
             new Product {Id=7, Name="prod7", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=7, Description="to jest prod7", CompanyID= pom1.ID },
             new Product {Id=8, Name="prod8", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=8, Description="to jest prod8", CompanyID= pom1.ID },
             new Product {Id=9, Name="prod9", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=9, Description="to jest prod9", CompanyID= pom1.ID },
             new Product {Id=10, Name="prod10", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=10, Description="to jest prod10", CompanyID= pom2.ID },
             new Product {Id=11, Name="prod11", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=11, Description="to jest prod11", CompanyID= pom2.ID, },
             new Product {Id=12, Name="prod12", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=12, Description="to jest prod12", CompanyID= pom2.ID },
             new Product {Id=13, Name="prod13", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=13, Description="to jest prod13", CompanyID= pom2.ID },
             new Product {Id=14, Name="prod14", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=14, Description="to jest prod14", CompanyID= pom2.ID },
             new Product {Id=15, Name="prod15", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=15, Description="to jest prod15", CompanyID= pom2.ID },
             new Product {Id=16, Name="prod16", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=16, Description="to jest prod16", CompanyID= pom2.ID },
             new Product {Id=17, Name="prod17", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=17, Description="to jest prod17", CompanyID= pom2.ID },
             new Product {Id=18, Name="prod18", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=18, Description="to jest prod18", CompanyID= pom3.ID },
             new Product {Id=19, Name="prod19", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=19, Description="to jest prod19", CompanyID= pom3.ID },
             new Product {Id=20, Name="prod20", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=20, Description="to jest prod20", CompanyID= pom3.ID },
             new Product {Id=21, Name="prod21", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=21, Description="to jest prod21", CompanyID= pom3.ID },
             new Product {Id=22, Name="prod22", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=22, Description="to jest prod22", CompanyID= pom3.ID },
             new Product {Id=23, Name="prod23", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=23, Description="to jest prod23", CompanyID= pom3.ID },
             new Product {Id=24, Name="prod24", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=24, Description="to jest prod24", CompanyID= pom3.ID },
             new Product {Id=25, Name="prod25", Image = File.ReadAllBytes("wwwroot/photos/noone.png"), Price=25, Description="to jest prod25", CompanyID= pom3.ID } 
            );




        }

    }
}
