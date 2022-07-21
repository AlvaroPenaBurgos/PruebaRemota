using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace PruebaRemota.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {

        }
        public DbSet<BooksItem> Books { get; set; } = null!;
    }
}
