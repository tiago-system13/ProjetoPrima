using aplicacao.Interfaces;
using aplicacao.ViewModel;
using aplicacao.ViewModel.Filtro;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ControleDeEstoque.Controllers
{
    public class VendaController : Controller
    {
        private readonly IServicoVendaAplicacao _servVenda;

        public VendaController(IServicoVendaAplicacao servVenda)
        {
            _servVenda = servVenda;
        }

        // GET: Venda
        public ActionResult Index()
        {
            var venda = new VendaViewModel();
            return View("RegistrarVenda", venda);
        }
        public ActionResult IndexHistoricoVenda()
        {
            var venda = new VendaViewModelFiltro();
            return View(venda);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(VendaViewModel formularioDeVenda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servVenda.Incluir(formularioDeVenda);
                    TempData["Sucesso"] = "Venda concluida com sucesso!";
                    TempData["Situacao"] = "Sucesso!";
                    return Json(new { Sucesso = true }, JsonRequestBehavior.AllowGet);
                }
                catch (ArgumentNullException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("RegistrarVenda", formularioDeVenda);
                }
                catch (ArgumentException v)
                {
                    TempData["Erro"] = v.Message;
                    TempData["Situacao"] = "Erro!";
                    return View("RegistrarVenda", formularioDeVenda);
                }
                catch (ValidationException v)
                {
                    ModelState.AddModelError(string.Empty, v.Message);
                    return View("RegistrarVenda", formularioDeVenda);
                }
            }

            return View("RegistrarVenda", formularioDeVenda);
        }

        [HttpGet]
        public JsonResult ObterHistoricoVenda(VendaViewModelFiltro dadosPesquisa)
        {
            var listaVendas = _servVenda.ObterVendas(dadosPesquisa);
            return Json(new
            {
                RegistrosPorPagina = listaVendas.NumeroDeRegistrosPorPagina,
                Pagina = listaVendas.IndiceDePagina,
                TotalDePaginas = listaVendas.TotalDePaginas,
                TotalDeRegistros = listaVendas.TotalDeRegistros,
                Lista = listaVendas.ToList(),
            },
              JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObterItemDaVenda(VendaViewModelFiltro dadosPesquisa)
        {
            var listaItemVenda = _servVenda.ObterItemVenda(dadosPesquisa);
            return Json(new
            {
                RegistrosPorPagina = listaItemVenda.NumeroDeRegistrosPorPagina,
                Pagina = listaItemVenda.IndiceDePagina,
                TotalDePaginas = listaItemVenda.TotalDePaginas,
                TotalDeRegistros = listaItemVenda.TotalDeRegistros,
                Lista = listaItemVenda.ToList(),
            },
              JsonRequestBehavior.AllowGet);
        }
    }
}