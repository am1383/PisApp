using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities.Common;

namespace PisApp.API.DbContextes
{
    public class PisAppDb : DbContext
    {
        public PisAppDb(DbContextOptions<PisAppDb> options)
            : base(options)
        {
            //
        }

        public DbSet<BaseEntity> UserResults { get; set; } 
        public DbSet<UserId> userIds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}