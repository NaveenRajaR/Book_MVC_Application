using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public DbSet<BookModel> books { get; set; }
    }
}
