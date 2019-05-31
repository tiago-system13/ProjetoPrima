using aplicacao.ViewModel;
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
