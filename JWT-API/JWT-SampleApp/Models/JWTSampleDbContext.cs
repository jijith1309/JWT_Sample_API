using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JWT_SampleApp.Models
{
    public class JWTSampleDbContext: DbContext
    {
        public JWTSampleDbContext() : base("JwtSampleAppConnection")
        {

        }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////Set StudentName as concurrency column
            ////modelBuilder.Entity<Student>()
            ////        .Property(p => p.LastUpdatedTime)
            ////        .IsConcurrencyToken();
            ////modelBuilder.HasDefaultSchema(ECommerce.Common.ConfigurationReader.ConfigurationReader.DefaultSchema);
            //modelBuilder.Configurations.Add(new ApplicationUserEntityConfiguration());

           // modelBuilder.Entity<Product>()
           //.Property(p => p.Price).HasPrecision()


        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
      
        

    }
}