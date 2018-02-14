
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FolhaCerta.Model.Domain;

namespace FolhaCerta.DataAccess.ConfigurationMap
{
    public class FuncionarioConfig : EntityTypeConfiguration<Funcionario>
    {
        public override void Map(EntityTypeBuilder<Funcionario> modelBuilder)
        {
            
            modelBuilder.Property(x => x.DataCriacao).ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Property(x => x.DataAlteracao).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");
            

            modelBuilder.ToTable("Funcionario");
        }
    }
}