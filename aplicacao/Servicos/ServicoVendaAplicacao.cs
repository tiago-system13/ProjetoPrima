using aplicacao.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using aplicacao.ViewModel;
using dominio.Interfaces;
using dominio.Models;
using aplicacao.ViewModel.Filtro;
using infraEstrutura;
using aplicacao.ViewModel.Grid;

namespace aplicacao.Servicos
{
    public class ServicoVendaAplicacao : IServicoVendaAplicacao
    {
        private IServicoVendaRepositorio _vendaRepositorio;

        public ServicoVendaAplicacao(IServicoVendaRepositorio vendaRepositorio)
        {
            _vendaRepositorio = vendaRepositorio;
        }

        private Venda Criar(VendaViewModel vendaViewModel)
        {

            var venda = new Venda()
            {
                Id = vendaViewModel.Id,
                DataDaVenda = DateTime.Now,
                TotalDaVenda = vendaViewModel.TotalDaVenda.Value,
                ClienteId = vendaViewModel.ClienteId.Value,
                ItensVenda = new List<ItemVendaProduto>()
            };

            foreach (var item in vendaViewModel.ItensVenda)
            {
                venda.ItensVenda.Add(new ItemVendaProduto() { ProdutoId = item.ProdutoId, Quantidade = item.Quantidade, TotalItem = item.TotalItem, Venda = venda });
            }

            return venda;

        }

        public void Incluir(VendaViewModel vendaViewModel)
        {
            _vendaRepositorio.Incluir(Criar(vendaViewModel));
        }

        public ListaPaginavel<VendaViewModelGrid> ObterVendas(VendaViewModelFiltro vendaFiltro)
        {
            var predicate = PredicateBuilder.True<Venda>();

            if (vendaFiltro.Cliente > 0)
            {

                predicate = predicate.And(v => v.ClienteId == vendaFiltro.Cliente);

            }

            if (!string.IsNullOrEmpty(vendaFiltro.DataDaVenda))
            {
                var data = Convert.ToDateTime(vendaFiltro.DataDaVenda);
                var dataFim = data.AddDays(1);
                predicate = predicate.And(v => v.DataDaVenda >= data && v.DataDaVenda < dataFim);
            }

            if (vendaFiltro.ProdutoId > 0)
            {
                predicate = predicate.And(v => v.ItensVenda.Any(it => it.ProdutoId == vendaFiltro.ProdutoId));
            }

            return _vendaRepositorio.ObterTodaVendas(predicate).Select(it => new VendaViewModelGrid()
            {
                Id = it.Id,
                ClienteId = it.ClienteId,
                DataDaVenda = it.DataDaVenda,
                TotalDaVenda = it.TotalDaVenda,
                NomeCliente = it.Cliente.Nome
            }).AsQueryable().ParaListaPaginavel(vendaFiltro.IndiceDePagina, vendaFiltro.RegistrosPorPagina, vendaFiltro.Ordenacao, x => x.DataDaVenda);
        }

        public ListaPaginavel<ItemVendaViewModelGrid> ObterItemVenda(VendaViewModelFiltro vendaFiltro)
        {

            var predicate = PredicateBuilder.True<Venda>();

            if (vendaFiltro.Id > 0)
            {
                predicate = predicate.And(v => v.Id == vendaFiltro.Id);
            }

            return _vendaRepositorio.ObterTodaVendas(predicate).SelectMany(it => it.ItensVenda).Select(it => new ItemVendaViewModelGrid()
            {
                NomeProduto = it.Produto.NomeDoProduto,
                Quantidade = it.Quantidade,
                TotalItem = it.TotalItem
            }).AsQueryable().ParaListaPaginavel(vendaFiltro.IndiceDePagina, vendaFiltro.RegistrosPorPagina, vendaFiltro.Ordenacao, x => x.NomeProduto);

        }
    }
}
