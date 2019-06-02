using dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using dominio.Models;
using System.Linq.Expressions;
using infraEstrutura;

namespace dominio.Servicos
{
    public class ServicoClienteRepositorio : IServicoClienteRepositorio
    {
        private readonly IRepositorio<Cliente, int> _cliente;

        public ServicoClienteRepositorio(IRepositorio<Cliente, int> cliente)
        {

            _cliente = cliente;
        }

        public void Editar(Cliente cliente)
        {
            var clienteExistente = _cliente.Todos.FirstOrDefault(c => c.Id == cliente.Id);

            if (clienteExistente == null)
            {

                throw new ArgumentNullException("Cliente Inexistente");
            }

            var clientePorCpf = _cliente.Todos.FirstOrDefault(c => c.Cpf.Equals(cliente.Cpf));

            if (clientePorCpf != null && clientePorCpf.Id != cliente.Id)
            {

                throw new ArgumentException("Já existe registro deste CPF");
            }

            clienteExistente.Cpf = cliente.Cpf;
            clienteExistente.DataDeNascimento = cliente.DataDeNascimento;
            clienteExistente.Email = cliente.Email;
            clienteExistente.Telefone = cliente.Telefone;
            clienteExistente.Nome = cliente.Nome;

            _cliente.Editar(clienteExistente);
            _cliente.Salvar();
        }

        public void Excluir(int id)
        {
            if (_cliente.Todos.Any(c => c.VendasDoCliente.Any(v => v.ClienteId == id)))
            {

                throw new ArgumentException("o cliente não pode ser excluido, pois existe registro de vendas");
            }

            _cliente.Excluir(id);
            _cliente.Salvar();
        }

        public void Incluir(Cliente cliente)
        {

            if (_cliente.Todos.Any(c => c.Cpf.Equals(cliente.Cpf))) throw new ArgumentException("Já existe registro deste CPF");

            _cliente.Inserir(cliente);
            _cliente.Salvar();
        }

        public Cliente Obtercliente(int clienteId)
        {
            return _cliente.Todos.FirstOrDefault(c => c.Id == clienteId);
        }

        public List<Cliente> ObterTodosClientes(Expression<Func<Cliente, bool>> predicate)
        {
            return _cliente.Todos.Where(predicate).OrderBy(c => c.Nome).ToList();
        }
    }
}
