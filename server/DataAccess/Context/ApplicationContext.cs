using FolhaCerta.Model.Domain;
using FolhaCerta.DataAccess.ConfigurationMap;
using Microsoft.EntityFrameworkCore;


namespace FolhaCerta.DataAccess.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

       
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new UsuarioConfig());
            modelBuilder.AddConfiguration(new FuncionarioConfig());
            modelBuilder.AddConfiguration(new DispositivoConfig());
            modelBuilder.AddConfiguration(new NotificacaoConfig());
        }
    }
}
