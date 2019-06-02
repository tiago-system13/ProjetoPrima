using aplicacao.Interfaces;
using aplicacao.ViewModel;
using aplicacao.ViewModel.Filtro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleDeEstoque.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IServicoClienteAplicacao _servClienteAplicacao;

        public ClienteController(IServicoClienteAplicacao servClienteAplicacao)
        {
            _servClienteAplicacao = servClienteAplicacao;
        }

        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NovoCliente()
        {
            var cliente = new ClienteViewModel();
            return View("Cadastrar", cliente);
        }

        public ActionResult IndexEditar(int id)
        {
            var cliente = _servClienteAplicacao.Obtercliente(id);
            return View("Editar", cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(ClienteViewModel formularioDeClienete)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servClienteAplicacao.Incluir(formularioDeClienete);
                    TempData["Sucesso"] = "Cadastro de cliente realizado com sucesso!";
                    TempData["Situacao"] = "Sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ArgumentNullException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Cadastrar", formularioDeClienete);
                }
                catch (ArgumentException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Cadastrar", formularioDeClienete);
                }
                catch (ValidationException v)
                {
                    ModelState.AddModelError(string.Empty, v.Message);
                    return View("Cadastrar", formularioDeClienete);
                }
            }

            return View("Cadastrar", formularioDeClienete);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ClienteViewModel formularioDeCliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servClienteAplicacao.Editar(formularioDeCliente);
                    TempData["Sucesso"] = "Atualização do clienete realizado com sucesso!";
                    TempData["Situacao"] = "Sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ValidationException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Editar", formularioDeCliente);
                }
                catch (ArgumentNullException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Editar", formularioDeCliente);
                }
                catch (ArgumentException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Editar", formularioDeCliente);
                }
            }
            return View("Editar", formularioDeCliente);
        }

        [HttpGet]
        public JsonResult ObterClientes(ClienteViewModelFiltro dadosPesquisa)
        {
            var listaClientes = _servClienteAplicacao.ObterTodosClientes(dadosPesquisa);
            return Json(new
            {
                RegistrosPorPagina = listaClientes.NumeroDeRegistrosPorPagina,
                Pagina = listaClientes.IndiceDePagina,
                TotalDePaginas = listaClientes.TotalDePaginas,
                TotalDeRegistros = listaClientes.TotalDeRegistros,
                Lista = listaClientes.ToList(),
            },
              JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            try
            {
                _servClienteAplicacao.Excluir(id);

                TempData["Sucesso"] = "Cliente excluido com sucesso!";
                TempData["Situacao"] = "Sucesso!";
                return Json(new { sucesso = true, mensagem = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {

                return Json(new { sucesso = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult ObterClientePorNome(string nomeDocumento)
        {
            var lista = _servClienteAplicacao.ListarClientes(nomeDocumento);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

    }
}