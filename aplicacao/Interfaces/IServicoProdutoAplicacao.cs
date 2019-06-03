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
    public interface IServicoProdutoAplicacao
    {
        void Incluir(ProdutoViewModel Produto);
        void Editar(ProdutoViewModel Produto);
        void Excluir(int id);
        ProdutoViewModel ObterProduto(int Produto);
        ListaPaginavel<ProdutoViewModel> ObterTodosProdutos(ProdutoViewModelFiltro ProdutoViewModel);
        IEnumerable<PordutoViewModelList> ListarProdutosPorNome(string nome);

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
    public interface IServicoProdutoAplicacao
    {
        void Incluir(ProdutoViewModel Produto);
        void Editar(ProdutoViewModel Produto);
        void Excluir(int id);
        ProdutoViewModel ObterProduto(int Produto);
        ListaPaginavel<ProdutoViewModel> ObterTodosProdutos(ProdutoViewModelFiltro ProdutoViewModel);
        IEnumerable<PordutoViewModelList> ListarProdutosPorNome(string nome);

    }
}
>>>>>>> fad11f3e7c10c01b8efe32fd0d28703c9204a75f
