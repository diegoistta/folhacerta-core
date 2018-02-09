using FolhaCerta.Model.Domain;
using FolhaCerta.DataAccess.ConfigurationMap;
using Microsoft.EntityFrameworkCore;


namespace FolhaCerta.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new UsuarioConfiguration());
        }
    }
}
