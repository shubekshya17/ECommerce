using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductOrderMaster> ProductOrderMaster { get; set; }
        public DbSet<ProductOrderDetail> ProductOrderDetail { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "ShubekshyaShrestha",
                    Password = "Pass123$",
                }
                );
        }
    }
}
