using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Entities.Common;

namespace PisApp.API.DbContextes
{
    public class PisAppDbContext : DbContext
    {
        public PisAppDbContext(DbContextOptions<PisAppDbContext> options)
            : base(options)
        {
            //
        }

        public DbSet<BaseEntity> baseEntities { get; set; } 
        public DbSet<VIPUser> vIPUsers { get; set; }
        public DbSet<Discount> discounts { get; set; }
        public DbSet<Refer> refers { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<UserProfit> UserProfit { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<UserDetail> userDetails { get; set; }
        public DbSet<PrivateDiscount> privateDiscounts { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VIPUser>      ().HasNoKey();
            modelBuilder.Entity<User>         ().HasNoKey();
            modelBuilder.Entity<Cart>         ().HasNoKey();
            modelBuilder.Entity<Discount>     ().HasNoKey();
            modelBuilder.Entity<ShoppingCart> ().HasNoKey();
            modelBuilder.Entity<Address>      ().HasNoKey();
            modelBuilder.Entity<Refer>        ().HasNoKey();
            modelBuilder.Entity<UserDetail>   ().HasNoKey();
            modelBuilder.Entity<PrivateDiscount>().HasNoKey();
            modelBuilder.Entity<UserProfit>()     .HasNoKey();
        }
    }
}