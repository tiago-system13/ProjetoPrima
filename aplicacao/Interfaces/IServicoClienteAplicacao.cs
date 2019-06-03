<<<<<<< HEAD
﻿using aplicacao.ViewModel;
using aplicacao.ViewModel.Filtro;
using aplicacao.ViewModel.List;
using infraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.Interfaces
{
    public interface IServicoClienteAplicacao
    {
        void Incluir(ClienteViewModel cliente);
        void Editar(ClienteViewModel cliente);
        void Excluir(int id);
        ClienteViewModel Obtercliente(int cliente);
        ListaPaginavel<ClienteViewModel> ObterTodosClientes(ClienteViewModelFiltro cliente);
        IEnumerable<ClienteViewModelList> ListarClientes(string nomeDocumento);
    }
}
=======
﻿using aplicacao.ViewModel;
using aplicacao.ViewModel.Filtro;
using aplicacao.ViewModel.List;
using infraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aplicacao.Interfaces
{
   public interface IServicoClienteAplicacao
    {
        void Incluir(ClienteViewModel cliente);
        void Editar(ClienteViewModel cliente);
        void Excluir(int id);
        ClienteViewModel Obtercliente(int cliente);
        ListaPaginavel<ClienteViewModel> ObterTodosClientes(ClienteViewModelFiltro cliente);
        IEnumerable<ClienteViewModelList> ListarClientes(string nomeDocumento);
    }
}
>>>>>>> fad11f3e7c10c01b8efe32fd0d28703c9204a75f
