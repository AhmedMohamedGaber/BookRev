using BookRev.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BookRev.Models
{
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
          
        }

        public  DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authorities { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }

        internal void Add<T>(BooksController boo)
        {
            throw new NotImplementedException();
        }
    }
}
