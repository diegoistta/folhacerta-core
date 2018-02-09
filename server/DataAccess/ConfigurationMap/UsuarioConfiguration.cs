
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FolhaCerta.Model.Domain;

namespace FolhaCerta.DataAccess.ConfigurationMap
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public override void Map(EntityTypeBuilder<Usuario> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.HasQueryFilter(x => !x.Inativo);
            
            modelBuilder.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
            modelBuilder.Property(x => x.DataCriacao).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Property(x => x.DataAlteracao).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");;

            modelBuilder.ToTable("Usuario");
        }
    }
}