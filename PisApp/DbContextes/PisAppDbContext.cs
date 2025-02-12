using Microsoft.EntityFrameworkCore;
using PisApp.API.DTOs;
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

        public DbSet<BaseEntity> UserResults { get; set; } 
        public DbSet<VIPUser> vIPUsers { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<UserDetailDto> UserDetailDtos { get; set; }
        public DbSet<User> userIds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VIPUser>().HasNoKey();
            modelBuilder.Entity<Address>().HasNoKey();
        }
    }
}