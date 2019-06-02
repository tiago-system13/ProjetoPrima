using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.ViewModel.Grid
{
   public class VendaViewModelGrid
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public string NomeCliente { get; set; }

        public DateTime DataDaVenda { get; set; }
        
        public decimal TotalDaVenda { get; set; }        
    }
}
