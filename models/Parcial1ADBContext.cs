using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.models
{
    public class Parcial1ADBContext : DbContext
    {

        public Parcial1ADBContext(DbContextOptions<Parcial1ADBContext> options) : base(options)
        {

        }

        public DbSet<Autores> Autores { get; set; }
        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Posts> Posts { get; set; }

    }
}
