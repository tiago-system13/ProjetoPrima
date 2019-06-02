using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using dominio.Models;

namespace dominio.Mappins
{
    public class ItemVendaProdutoMapping : EntityTypeConfiguration<ItemVendaProduto>
    {
        public ItemVendaProdutoMapping()
        {
            Property(x => x.Id)
               .HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Quantidade)
                .HasColumnName("quantidade").IsRequired();

            Property(x => x.TotalItem)
               .HasColumnName("total_item").IsRequired();

            Property(x => x.ProdutoId)
                .HasColumnName("produto_id").IsRequired();

            Property(x => x.VendaId)
                .HasColumnName("venda_id").IsRequired();

            ToTable("tb_item_venda_produto", "estoque");
        }
    }
}
