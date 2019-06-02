using infraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.ViewModel.Filtro
{
   public class ClienteViewModelFiltro: PesquisaPorPagina
    {
        public string Cpf { get; set; }

        public string Nome { get; set; }
    }
}
