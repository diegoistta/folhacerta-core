
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FolhaCerta.Model.Domain;

namespace FolhaCerta.DataAccess.ConfigurationMap
{
    public class DispositivoConfig : EntityTypeConfiguration<Dispositivo>
    {
        public override void Map(EntityTypeBuilder<Dispositivo> modelBuilder)
        {   
            modelBuilder.Property(x => x.DataCriacao).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Property(x => x.DataAlteracao).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");
            

            modelBuilder.ToTable("Dispositivo");
        }
    }
}