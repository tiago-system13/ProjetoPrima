using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using dominio.Models;

namespace dominio.Mappins
{
    public class VendaMapping : EntityTypeConfiguration<Venda>
    {
        public VendaMapping()
        {
            Property(x => x.Id)
               .HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ClienteId)
               .HasColumnName("cliente_id").IsRequired();

            Property(x => x.DataDaVenda)
               .HasColumnName("dt_venda").IsRequired();

            Property(x => x.TotalDaVenda)
             .HasColumnName("total_venda").IsRequired();

            ToTable("tb_venda", "estoque");
        }
    }
}
