
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FolhaCerta.Model.Domain;

namespace FolhaCerta.DataAccess.ConfigurationMap
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public override void Map(EntityTypeBuilder<Usuario> modelBuilder)
        {
             modelBuilder.HasKey(x => x.Id);
            
            modelBuilder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Property(x => x.DataCriacao).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Property(x => x.DataAlteracao).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Property(x => x.Email).IsRequired().HasMaxLength(256);
            
            modelBuilder.HasOne(x => x.CadastroFuncionario)
            .WithOne(y => y.Usuario)
            .HasForeignKey<Funcionario>(f => f.UsuarioId);

            modelBuilder.HasOne(x => x.Notificacao)
            .WithOne(y => y.Usuario)
            .HasForeignKey<Notificacao>(f => f.UsuarioId);

             modelBuilder.HasOne(x => x.DispositivoMovel)
            .WithOne(y => y.Usuario)
            .HasForeignKey<Dispositivo>(f => f.UsuarioId);

            modelBuilder.ToTable("Usuario");
        }
    }
}