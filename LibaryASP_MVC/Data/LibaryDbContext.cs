using LibaryASP_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibaryASP_MVC.Data
{
    public class LibaryDbContext : DbContext
    {
        public LibaryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
