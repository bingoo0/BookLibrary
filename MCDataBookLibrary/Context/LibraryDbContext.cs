using MCDataBookLibrary.Entities;
using System.Data.Entity;


namespace MCDataBookLibrary.Context
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Pictures> Pictures { get; set; }


    }
}
