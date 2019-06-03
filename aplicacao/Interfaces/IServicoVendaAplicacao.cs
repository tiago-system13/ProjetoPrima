<<<<<<< HEAD
﻿using aplicacao.ViewModel;
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
=======
﻿using aplicacao.ViewModel;
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
>>>>>>> fad11f3e7c10c01b8efe32fd0d28703c9204a75f
