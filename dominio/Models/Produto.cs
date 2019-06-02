using infraEstrutura;
using System.Collections.Generic;

namespace dominio.Models
{
    public class Produto : IEntidade<int>
    {
        public int Id { get; set; }
        public string NomeDoProduto { get; set; }
        public int QuantidadeDoProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        public virtual ICollection<ItemVendaProduto> ItensVendasProduto { get; set; }
    }
}
