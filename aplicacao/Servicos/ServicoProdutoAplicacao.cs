using aplicacao.Interfaces;
using System;
using System.Collections.Generic;
using aplicacao.ViewModel;
using dominio.Models;
using dominio.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using aplicacao.ViewModel.List;
using infraEstrutura;
using aplicacao.ViewModel.Filtro;

namespace aplicacao.Servicos
{
    public class ServicoProdutoAplicacao : IServicoProdutoAplicacao
    {
        private readonly IServicoProdutoRepositorio _produtoRepositorio;

        public ServicoProdutoAplicacao(IServicoProdutoRepositorio produtoRepositorio)
        {

            _produtoRepositorio = produtoRepositorio;
        }

        private Produto Criar(ProdutoViewModel produto)
        {
            return new Produto()
            {
                Id = produto.Id,
                NomeDoProduto = produto.NomeDoProduto,
                QuantidadeDoProduto = produto.QuantidadeDoProduto.Value,
                ValorUnitario = produto.ValorUnitario.Value
            };
        }


        public void Editar(ProdutoViewModel produto)
        {
            var produtoCadastrado = _produtoRepositorio.ObterProdutoPorNome(produto.NomeDoProduto);

            if (produtoCadastrado != null && produto.Id != produtoCadastrado.Id)
            {

                throw new ArgumentException($"Existe produto cadastrado com esse nome: {produto.NomeDoProduto} ");
            }

            _produtoRepositorio.Editar(Criar(produto));
        }

        public void Incluir(ProdutoViewModel produto)
        {
            if (_produtoRepositorio.ExisteProdutoPorNome(produto.NomeDoProduto))
            {

                throw new ArgumentException($"Existe produto cadastrado com esse nome: {produto.NomeDoProduto} ");
            }

            _produtoRepositorio.Incluir(Criar(produto));
        }

        public ProdutoViewModel ObterProduto(int produtoId)
        {
            var produto = _produtoRepositorio.ObterProduto(produtoId);
            return new ProdutoViewModel()
            {
                Id = produto.Id,
                NomeDoProduto = produto.NomeDoProduto,
                QuantidadeDoProduto = produto.QuantidadeDoProduto,
                ValorUnitario = produto.ValorUnitario
            };
        }

        public ListaPaginavel<ProdutoViewModel> ObterTodosProdutos(ProdutoViewModelFiltro ProdutoViewModel)
        {
            var produto = new Produto();

            if (ProdutoViewModel != null)
            {

                if (ProdutoViewModel.NomeProduto != null)
                {
                    produto.NomeDoProduto = ProdutoViewModel.NomeProduto;
                }
            }

            var produtos = _produtoRepositorio.ObterTodosProdutos(produto);

            return produtos.Select(c => new ProdutoViewModel() { Id = c.Id, NomeDoProduto = c.NomeDoProduto, QuantidadeDoProduto = c.QuantidadeDoProduto, ValorUnitario = c.ValorUnitario })
                 .AsQueryable().ParaListaPaginavel(ProdutoViewModel.IndiceDePagina, ProdutoViewModel.RegistrosPorPagina, ProdutoViewModel.Ordenacao, x => x.NomeDoProduto);
        }

        public IEnumerable<PordutoViewModelList> ListarProdutosPorNome(string nome)
        {
            var produto = new ProdutoViewModelFiltro() { NomeProduto = nome };

            return ObterTodosProdutos(produto).Select(it => new PordutoViewModelList() { Id = it.Id, NomeDoProduto = it.NomeDoProduto });
        }

        public void Excluir(int id)
        {
            _produtoRepositorio.Excluir(id);
        }
    }
}
