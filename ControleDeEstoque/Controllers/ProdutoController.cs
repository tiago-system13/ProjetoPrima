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
    public class ProdutoController : Controller
    {
        private readonly IServicoProdutoAplicacao _servProduto;

        public ProdutoController(IServicoProdutoAplicacao servProduto)
        {
            _servProduto = servProduto;
        }

        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NovoProduto()
        {
            var produto = new ProdutoViewModel();
            return View("Cadastrar", produto);
        }

        public ActionResult IndexEditar(int id)
        {
            var produto = _servProduto.ObterProduto(id);
            return View("Editar", produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(ProdutoViewModel formularioDeProduto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servProduto.Incluir(formularioDeProduto);
                    TempData["Sucesso"] = "Produto cadastrado com sucesso!";
                    TempData["Situacao"] = "Sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ArgumentNullException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Cadastrar", formularioDeProduto);
                }
                catch (ArgumentException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Cadastrar", formularioDeProduto);
                }
                catch (ValidationException v)
                {
                    ModelState.AddModelError(string.Empty, v.Message);
                    return View("Cadastrar", formularioDeProduto);
                }
            }

            return View("Cadastrar", formularioDeProduto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ProdutoViewModel formularioDeProduto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servProduto.Editar(formularioDeProduto);
                    TempData["Sucesso"] = "Produto atualizado com sucesso!";
                    TempData["Situacao"] = "Sucesso!";
                    return RedirectToAction("Index");
                }
                catch (ValidationException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Editar", formularioDeProduto);
                }
                catch (ArgumentNullException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Editar", formularioDeProduto);
                }
                catch (ArgumentException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("Editar", formularioDeProduto);
                }
            }
            return View("Editar", formularioDeProduto);
        }

        [HttpGet]
        public JsonResult ObterProdutos(ProdutoViewModelFiltro dadosPesquisa)
        {
            var listaDeProdutos = _servProduto.ObterTodosProdutos(dadosPesquisa);
            return Json(new
            {
                RegistrosPorPagina = listaDeProdutos.NumeroDeRegistrosPorPagina,
                Pagina = listaDeProdutos.IndiceDePagina,
                TotalDePaginas = listaDeProdutos.TotalDePaginas,
                TotalDeRegistros = listaDeProdutos.TotalDeRegistros,
                Lista = listaDeProdutos.ToList(),
            },
              JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterProdutoPorNome(string nome)
        {
            var lista = _servProduto.ListarProdutosPorNome(nome);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObterProdutoPorId(int produtoId)
        {
            var produto = _servProduto.ObterProduto(produtoId);
            return Json(produto, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Excluir(int id)
        {
            try
            {
                _servProduto.Excluir(id);

                TempData["Sucesso"] = "Produto excluido com sucesso!";
                TempData["Situacao"] = "Sucesso!";
                return Json(new { sucesso = true, mensagem = "" }, JsonRequestBehavior.AllowGet);

            }
            catch (ArgumentException ex)
            {

                return Json(new { sucesso = false, mensagem = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}