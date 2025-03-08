using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;
using PisApp.API.Entities.Common;
using PisApp.API.Products.Entities;
using PisApp.API.Products.Entities.Common;

namespace PisApp.API.DbContextes
{
    public class PisAppDbContext : DbContext
    {
        public PisAppDbContext(DbContextOptions<PisAppDbContext> options)
            : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VIPUser>        ().HasNoKey();
            modelBuilder.Entity<Motherboard>    ().HasNoKey();
            modelBuilder.Entity<Cpu>            ().HasNoKey();
            modelBuilder.Entity<Gpu>            ().HasNoKey();
            modelBuilder.Entity<Cooler>         ().HasNoKey();
            modelBuilder.Entity<Ssd>            ().HasNoKey();
            modelBuilder.Entity<Hdd>            ().HasNoKey();
            modelBuilder.Entity<Product>        ().HasNoKey();
            modelBuilder.Entity<CartItem>       ().HasNoKey();
            modelBuilder.Entity<VIPCheckResult> ().HasNoKey();
            modelBuilder.Entity<UserRefferCode> ().HasNoKey();
            modelBuilder.Entity<CommonProduct>  ().HasNoKey();
            modelBuilder.Entity<CartItemProduct>().HasNoKey();
            modelBuilder.Entity<Compatible>     ().HasNoKey();
            modelBuilder.Entity<UserLogin>      ().HasNoKey();
            modelBuilder.Entity<User>           ().HasNoKey();
            modelBuilder.Entity<Cart>           ().HasNoKey();
            modelBuilder.Entity<Discount>       ().HasNoKey();
            modelBuilder.Entity<ShoppingCart>   ().HasNoKey();
            modelBuilder.Entity<Address>        ().HasNoKey();
            modelBuilder.Entity<Refer>          ().HasNoKey();
            modelBuilder.Entity<UserDetail>     ().HasNoKey();
            modelBuilder.Entity<PrivateDiscount>().HasNoKey();
            modelBuilder.Entity<UserProfit>     ().HasNoKey();
        }
    }
}