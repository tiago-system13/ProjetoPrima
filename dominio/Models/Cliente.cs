using infraEstrutura;
using System;
using System.Collections.Generic;

namespace dominio.Models
{
    public class Cliente : IEntidade<int>
    {
        public int Id { get; set; }

        public string Cpf { get; set; }

        public string Nome { get; set; }

        public DateTime? DataDeNascimento { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Venda> VendasDoCliente { get; set; }

    }
}
