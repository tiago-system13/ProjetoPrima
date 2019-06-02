using dominio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace dominio.Mappins
{
    public class ClienteMapping : EntityTypeConfiguration<Cliente>
    {
        public ClienteMapping()
        {
            Property(x => x.Id)
               .HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Cpf)
                .HasColumnName("cpf").HasMaxLength(11).IsRequired();

            Property(x => x.Nome)
                 .HasColumnName("nome").HasMaxLength(80).IsRequired();

            Property(x => x.DataDeNascimento)
               .HasColumnName("dt_nascimento");

            Property(x => x.Email)
                .HasColumnName("email");

            Property(x => x.Telefone)
                 .HasColumnName("telefone");

            ToTable("tb_cliente", "estoque");
        }
    }
}
