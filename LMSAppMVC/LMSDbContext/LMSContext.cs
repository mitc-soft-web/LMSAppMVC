using LMSAppMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSAppMVC.LMSDbContext
{
    public class LMSContext(DbContextOptions<LMSContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Book> Books => Set<Book>();
    }
}
