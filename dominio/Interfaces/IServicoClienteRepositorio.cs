using dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dominio.Interfaces
{
   public interface IServicoClienteRepositorio
    {
        void Incluir(Cliente cliente);
        void Editar(Cliente cliente);
        void Excluir(int id);
        Cliente Obtercliente(int cliente);
        List<Cliente> ObterTodosClientes(Expression<Func<Cliente, bool>> predicate);
    }
}
