using infraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.ViewModel.Filtro
{
   public class VendaViewModelFiltro:PesquisaPorPagina
    {
        public int Id { get; set; }

        public int Cliente { get; set; }

        public int ProdutoId { get; set; }

        public string DataDaVenda { get; set; }

    }
}
