using infraEstrutura;
using System;
using System.Collections.Generic;

namespace dominio.Models
{
    public class Venda : IEntidade<int>
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public DateTime DataDaVenda { get; set; }

        public virtual ICollection<ItemVendaProduto> ItensVenda { get; set; }

        public decimal TotalDaVenda { get; set; }

        public virtual Cliente Cliente { get; set; }

    }
}
