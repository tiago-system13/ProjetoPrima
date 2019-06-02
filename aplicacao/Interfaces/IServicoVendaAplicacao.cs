using aplicacao.ViewModel;
using aplicacao.ViewModel.Filtro;
using aplicacao.ViewModel.Grid;
using infraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.Interfaces
{
    public interface IServicoVendaAplicacao
    {
        void Incluir(VendaViewModel vendaViewModel);

        ListaPaginavel<VendaViewModelGrid> ObterVendas(VendaViewModelFiltro vendaFiltro);

        ListaPaginavel<ItemVendaViewModelGrid> ObterItemVenda(VendaViewModelFiltro vendaFiltro);
    }
}
