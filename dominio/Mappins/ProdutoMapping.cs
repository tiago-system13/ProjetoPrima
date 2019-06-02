using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using dominio.Models;

namespace dominio.Mappins
{
    public class ProdutoMapping : EntityTypeConfiguration<Produto>
    {
        public ProdutoMapping()
        {
            Property(x => x.Id)
               .HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.NomeDoProduto)
                .HasColumnName("nome").HasMaxLength(60).IsRequired();

            Property(x => x.QuantidadeDoProduto)
                .HasColumnName("quantidade").IsRequired();

            Property(x => x.ValorUnitario)
                .HasColumnName("preco_unitario").IsRequired();


            ToTable("tb_produto", "estoque");
        }
    }
}
