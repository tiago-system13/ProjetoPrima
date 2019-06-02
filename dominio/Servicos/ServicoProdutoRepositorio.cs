using dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio.Models;
using System.Linq.Expressions;
using infraEstrutura;

namespace dominio.Servicos
{
    public class ServicoProdutoRepositorio : IServicoProdutoRepositorio
    {
        private readonly IRepositorio<Produto, int> _produto;

        public ServicoProdutoRepositorio(IRepositorio<Produto, int> produto)
        {
            _produto = produto;
        }

        public void Editar(Produto produto)
        {
            var produtoExistente = _produto.Todos.FirstOrDefault(c => c.Id == produto.Id);

            if (produtoExistente == null)
            {
                throw new ArgumentNullException("Produto Inexistente");
            }

            produtoExistente.NomeDoProduto = produto.NomeDoProduto;
            produtoExistente.QuantidadeDoProduto = produto.QuantidadeDoProduto;
            produtoExistente.ValorUnitario = produto.ValorUnitario;

            _produto.Editar(produtoExistente);
            _produto.Salvar();
        }

        public bool ExisteProdutoPorNome(string nome)
        {
            return _produto.Todos.Any(c => c.NomeDoProduto.ToUpper().Equals(nome.ToUpper()));
        }

        public void Incluir(Produto produto)
        {
            _produto.Inserir(produto);
            _produto.Salvar();
        }

        public Produto ObterProduto(int produtoId)
        {
            return _produto.Todos.FirstOrDefault(c => c.Id == produtoId);
        }

        public IEnumerable<Produto> ObterTodosProdutos(Produto produto)
        {
            if (!string.IsNullOrEmpty(produto.NomeDoProduto))
            {

                return _produto.Todos.Where(p => p.NomeDoProduto.ToUpper().Contains(produto.NomeDoProduto.ToUpper())).OrderBy(p => p.NomeDoProduto).ToList();
            }

            return _produto.Todos.OrderBy(p => p.NomeDoProduto).ToList();
        }

        public Produto ObterProdutoPorNome(string nome)
        {

            return _produto.Todos.FirstOrDefault(p => p.NomeDoProduto.ToUpper().Equals(nome.ToUpper()));
        }

        public void Excluir(int id)
        {
            if (_produto.Todos.Any(c => c.ItensVendasProduto.Any(v => v.ProdutoId == id)))
            {

                throw new ArgumentException("o produto não pode ser excluido, pois existe registro de vendas");
            }

            _produto.Excluir(id);
            _produto.Salvar();
        }
    }
}
