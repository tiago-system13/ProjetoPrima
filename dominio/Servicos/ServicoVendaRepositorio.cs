using dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using dominio.Models;
using System.Linq.Expressions;
using infraEstrutura;

namespace dominio.Servicos
{
    public class ServicoVendaRepositorio : IServicoVendaRepositorio
    {
        private readonly IRepositorio<Venda, int> _venda;
        private readonly IRepositorio<Produto, int> _produto;

        public ServicoVendaRepositorio(IRepositorio<Venda, int> venda, IRepositorio<Produto, int> produto)
        {
            _venda = venda;
            _produto = produto;
        }
        public void Incluir(Venda venda)
        {
            _venda.Inserir(venda);

            foreach (var item in venda.ItensVenda)
            {
                var produto = _produto.Todos.FirstOrDefault(p => p.Id == item.ProdutoId);
                produto.QuantidadeDoProduto -= item.Quantidade;
                _produto.Editar(produto);
            }

            _venda.Salvar();
        }

        public IEnumerable<Venda> ObterTodaVendas(Expression<Func<Venda, bool>> predicate)
        {
            return _venda.Todos.Where(predicate).Distinct().OrderByDescending(c => c.DataDaVenda).ToList();
        }

    }
}
