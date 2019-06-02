using dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dominio.Interfaces
{
    public interface IServicoProdutoRepositorio
    {
        void Incluir(Produto produto);
        void Editar(Produto produto);
        void Excluir(int id);
        Produto ObterProduto(int produtoId);
        Produto ObterProdutoPorNome(string nome);
        IEnumerable<Produto> ObterTodosProdutos(Produto produto);
        bool ExisteProdutoPorNome(string nome);       
    }
}
