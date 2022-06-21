using CoinConvertor.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoinConvertor.Context
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ConversionRequest> ConversionRequests { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<ConversionRequest>().ToTable("ConversionRequest");

        }
    }
}
