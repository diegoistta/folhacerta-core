
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FolhaCerta.Model.Domain;

namespace FolhaCerta.DataAccess.ConfigurationMap
{
    public class NotificacaoConfig : EntityTypeConfiguration<Notificacao>
    {
        public override void Map(EntityTypeBuilder<Notificacao> modelBuilder)
        {
           
            modelBuilder.Property(x => x.DataCriacao).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Property(x => x.DataAlteracao).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");
            

            modelBuilder.ToTable("Notificacao");
        }
    }
}