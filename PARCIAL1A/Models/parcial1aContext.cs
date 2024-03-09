namespace PARCIAL1A.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.SqlServer;
    using PARCIAL1A.Models;
    public class parcial1aContext : DbContext
    {
        public parcial1aContext(DbContextOptions<parcial1aContext> options) : base(options) { }

        public DbSet<autores> autores { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<autorlibro> autorlibro { get; set; }
        public DbSet<libros> libros { get; set; }
    }
}
