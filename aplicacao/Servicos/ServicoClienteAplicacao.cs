<<<<<<< HEAD
﻿using aplicacao.Interfaces;
using System;
using System.Collections.Generic;
using aplicacao.ViewModel;
using dominio.Models;
using dominio.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using aplicacao.ViewModel.List;
using aplicacao.ViewModel.Filtro;
using infraEstrutura;

namespace aplicacao.Servicos
{
    public class ServicoClienteAplicacao : IServicoClienteAplicacao
    {
        private readonly IServicoClienteRepositorio _clienteRepositorio;

        public ServicoClienteAplicacao(IServicoClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        private Cliente Criar(ClienteViewModel cliente)
        {

            return new Cliente()
            {
                Id = cliente.Id,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                DataDeNascimento = cliente.DataDeNascimento
            };
        }

        public void Editar(ClienteViewModel cliente)
        {
            _clienteRepositorio.Editar(Criar(cliente));
        }

        public void Incluir(ClienteViewModel cliente)
        {
            _clienteRepositorio.Incluir(Criar(cliente));
        }

        public ClienteViewModel Obtercliente(int clienteId)
        {
            var cliente = _clienteRepositorio.Obtercliente(clienteId);

            return new ClienteViewModel()
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                DataDeNascimento = cliente.DataDeNascimento,
                Telefone = cliente.Telefone
            };
        }

        public ListaPaginavel<ClienteViewModel> ObterTodosClientes(ClienteViewModelFiltro clienteFiltro)
        {

            var predicate = PredicateBuilder.True<Cliente>();

            if (!string.IsNullOrEmpty(clienteFiltro.Cpf))
            {
                predicate = predicate.And(v => v.Cpf.Contains(clienteFiltro.Cpf));

            }

            if (!string.IsNullOrEmpty(clienteFiltro.Nome))
            {
                predicate = predicate.And(v => v.Nome.ToUpper().Contains(clienteFiltro.Nome.ToUpper()));
            }

            var clientes = _clienteRepositorio.ObterTodosClientes(predicate);

            return clientes.Select(c => new ClienteViewModel() { Id = c.Id, Nome = c.Nome, Cpf = c.Cpf, Email = c.Email, Telefone = c.Telefone, DataDeNascimento = c.DataDeNascimento })
                           .AsQueryable().ParaListaPaginavel(clienteFiltro.IndiceDePagina, clienteFiltro.RegistrosPorPagina, clienteFiltro.Ordenacao, x => x.Nome);
        }

        public IEnumerable<ClienteViewModelList> ListarClientes(string nomeDocumento)
        {
            var numero = 0;
            var cliente = new ClienteViewModelFiltro();

            if (int.TryParse(nomeDocumento, out numero)) cliente.Cpf = nomeDocumento;

            else cliente.Nome = nomeDocumento;

            return ObterTodosClientes(cliente).Select(c => new ClienteViewModelList() { Id = c.Id, Nome = c.Cpf + " - " + c.Nome });
        }

        public void Excluir(int id)
        {
            _clienteRepositorio.Excluir(id);
        }
    }
}
=======
﻿using aplicacao.Interfaces;
using System;
using System.Collections.Generic;
using aplicacao.ViewModel;
using dominio.Models;
using dominio.Interfaces;
using System.Linq.Expressions;
using System.Linq;
using aplicacao.ViewModel.List;
using aplicacao.ViewModel.Filtro;
using infraEstrutura;

namespace aplicacao.Servicos
{
    public class ServicoClienteAplicacao : IServicoClienteAplicacao
    {
        private readonly IServicoClienteRepositorio _clienteRepositorio;

        public ServicoClienteAplicacao(IServicoClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        private Cliente Criar(ClienteViewModel cliente)
        {

            return new Cliente()
            {
                Id = cliente.Id,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                DataDeNascimento = cliente.DataDeNascimento
            };
        }

        public void Editar(ClienteViewModel cliente)
        {
            _clienteRepositorio.Editar(Criar(cliente));
        }

        public void Incluir(ClienteViewModel cliente)
        {
            _clienteRepositorio.Incluir(Criar(cliente));
        }

        public ClienteViewModel Obtercliente(int clienteId)
        {
            var cliente = _clienteRepositorio.Obtercliente(clienteId);

            return new ClienteViewModel()
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                DataDeNascimento = cliente.DataDeNascimento,
                Telefone = cliente.Telefone
            };
        }

        public ListaPaginavel<ClienteViewModel> ObterTodosClientes(ClienteViewModelFiltro clienteFiltro)
        {

            var predicate = PredicateBuilder.True<Cliente>();

            if (!string.IsNullOrEmpty(clienteFiltro.Cpf))
            {
                predicate = predicate.And(v => v.Cpf.Equals(clienteFiltro.Cpf));

            }

            if (!string.IsNullOrEmpty(clienteFiltro.Nome))
            {
                predicate = predicate.And(v => v.Nome.ToUpper().Contains(clienteFiltro.Nome.ToUpper()));
            }

            var clientes = _clienteRepositorio.ObterTodosClientes(predicate);

            return clientes.Select(c => new ClienteViewModel() { Id = c.Id, Nome = c.Nome, Cpf = c.Cpf, Email = c.Email, Telefone = c.Telefone, DataDeNascimento = c.DataDeNascimento })
                           .AsQueryable().ParaListaPaginavel(clienteFiltro.IndiceDePagina, clienteFiltro.RegistrosPorPagina, clienteFiltro.Ordenacao, x => x.Nome);
        }

        public IEnumerable<ClienteViewModelList> ListarClientes(string nomeDocumento)
        {
            var numero = 0;
            var cliente = new ClienteViewModelFiltro();

            if (int.TryParse(nomeDocumento, out numero)) cliente.Cpf = nomeDocumento;

            else cliente.Nome = nomeDocumento;

            return ObterTodosClientes(cliente).Select(c => new ClienteViewModelList() { Id = c.Id, Nome = c.Cpf + " - " + c.Nome });
        }

        public void Excluir(int id)
        {
            _clienteRepositorio.Excluir(id);
        }
    }
}
>>>>>>> fad11f3e7c10c01b8efe32fd0d28703c9204a75f
