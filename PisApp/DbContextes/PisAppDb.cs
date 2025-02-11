using Microsoft.EntityFrameworkCore;
using PisApp.API.Entities;

namespace PisApp.API.DbContextes
{
    public class PisAppDb : DbContext
    {
        public PisAppDb(DbContextOptions<PisAppDb> options)
            : base(options)
        {
            //
        }

        public required DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}