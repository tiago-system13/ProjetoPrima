using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infraEstrutura
{
    public interface IEntidade<T>
    {
        T Id { get;  set; }
    }
}
