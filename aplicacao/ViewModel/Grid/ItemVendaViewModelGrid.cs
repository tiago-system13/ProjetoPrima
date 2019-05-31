using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.ViewModel.Grid
{
   public class ItemVendaViewModelGrid
    {
        public int ProdutoId { get; set; }

        public string NomeProduto { get; set; }

        public int VendaId { get; set; }

        public int Quantidade { get; set; }

        public decimal TotalItem { get; set; }
    }
}
