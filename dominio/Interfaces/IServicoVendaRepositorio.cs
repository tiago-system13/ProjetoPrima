using dominio.Models;
using infraEstrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace dominio.Interfaces
{
  public interface IServicoVendaRepositorio
    {
        void Incluir(Venda venda);

        IEnumerable<Venda> ObterTodaVendas(Expression<Func<Venda, bool>> predicate);        
    }
}
