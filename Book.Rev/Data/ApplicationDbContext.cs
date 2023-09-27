using BookRev.Controllers;
using BookRev.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.Rev.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authorities { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookRev.Models.Book> Books { get; set; }

        internal void Add<T>(BooksController boo)
        {
            throw new NotImplementedException();
        }
    }
}
