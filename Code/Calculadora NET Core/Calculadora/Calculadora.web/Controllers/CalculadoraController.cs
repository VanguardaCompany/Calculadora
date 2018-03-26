using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Calculadora.Domain.Business;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using Calculadora.Domain.Models;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using PagedList.Core;
using PagedList.Core.Mvc;

namespace Calculadora.Web.Controllers
{
    public class CalculadoraController : Controller
    {
        ClientesBusiness clienteBusiness;

        CalculadoraBusiness calculadoraBusiness;

        public CalculadoraController(VanguardaContext context)
        {
            //_context = context;
            clienteBusiness = new ClientesBusiness(context);
            calculadoraBusiness = new CalculadoraBusiness(context);
            CalculadoraViewModel calculadoraViewModel = new CalculadoraViewModel();
        }

        // GET: Calculadora
        public ActionResult Index()
        {
            return View();
        }

        // GET: Calculadora/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Calculadora/Create
        public ActionResult CreateCalculadora()
        {
            CalculadoraViewModel model = new CalculadoraViewModel();
            model.Clientes = clienteBusiness.GetAllClientes().ToList();
            ViewBag.Clientes = model.Clientes.AsQueryable().ToPagedList(1, 3);

            SetSessionCalculadoraViewModel(model);

            return View(model);
        }

        // POST: Calculadora/Create
        [HttpPost]
        public ActionResult CreateCalculadora(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public PartialViewResult GetSimulacoes(int idCliente)
        {
            CalculadoraViewModel model = GetSessionCalculadoraViewModel();

            model.ClienteSelecionadoID = idCliente;
            model.ClienteSelecionado = clienteBusiness.GetClienteId(idCliente);
            model.Simulacoes = clienteBusiness.SimulacoesToViewModel(calculadoraBusiness.GetSimulacoes(idCliente)).ToList();

            SetSessionCalculadoraViewModel(model);

            return PartialView("_Simulacoes", model);
        }

        [HttpPost]
        public ActionResult SetSimulacaoAjax(int idSimulacao)
        {
            bool erro = true;
            try
            {
                CalculadoraViewModel model = GetSessionCalculadoraViewModel();

                SimulacaoViewModel simulacaoVM = new SimulacaoViewModel();
                if (idSimulacao > 0)
                {
                    simulacaoVM = calculadoraBusiness.SimulacaoToViewModel(calculadoraBusiness.GetSimulacaoId(idSimulacao));
                }
                else
                {

                    simulacaoVM.Data = DateTime.Now;
                    simulacaoVM.Cliente = model.ClienteSelecionado;
                }

                //calculadoraBusiness.SetSimulacao(ViewModelToSimulacao(simulacaoVM));
                model.SimulacaoSelecionada = simulacaoVM;

                SetSessionCalculadoraViewModel(model);

                erro = false;
            }
            catch (Exception)
            {

                throw;
            }


            return Json(erro);
        }

        [HttpPost]
        public PartialViewResult AddSimulacaoAjax()
        {
            try
            {
                CalculadoraViewModel model = GetSessionCalculadoraViewModel();

                SimulacaoViewModel simulacaoVM = new SimulacaoViewModel();

                simulacaoVM.Data = DateTime.Now;
                simulacaoVM.Cliente = model.ClienteSelecionado;
                model.SimulacaoSelecionada = simulacaoVM;

                calculadoraBusiness.AddSimulacao(simulacaoVM.SimulacaoToModel(simulacaoVM));

                SetSessionCalculadoraViewModel(model);

                return PartialView("_Simulacoes", model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public PartialViewResult GetTempoContribuicoesAjax()
        {
            CalculadoraViewModel model = GetSessionCalculadoraViewModel();

            if (model.SimulacaoSelecionada.SimulacaoID > 0)
            {
                //model.SimulacaoSelecionada = SimulacaoToViewModel(calculadoraBusiness.GetSimulacaoId(model.SimulacaoSelecionada.SimulacaoID));
                model.TempoContribuicoes = calculadoraBusiness.TempoContribuicoesToViewModel(calculadoraBusiness.GetTempoContribuicoes(model.SimulacaoSelecionada.SimulacaoID)).ToList();
            }
            else
            {

            }


            SetSessionCalculadoraViewModel(model);


            return PartialView("_TempoContribuicoes", model);
        }

        [HttpPost]
        public ActionResult SaveTempoContribuicaoAjax(TempoContribuicaoViewModel tempoContribuicaoVM)
        {
            CalculadoraViewModel model = GetSessionCalculadoraViewModel();

            string success = "true";

            if (!ModelState.IsValid)
            {
                success = "false";
            }
            else
            {
                tempoContribuicaoVM.SimulacaoID = model.SimulacaoSelecionada.SimulacaoID;

                model.TempoContribuicoes = new List<TempoContribuicaoViewModel>();
                model.TempoContribuicoes.Add(tempoContribuicaoVM);

                if (tempoContribuicaoVM.TempoContribuicaoID > 0) calculadoraBusiness.UpdateTempoContribuicao(tempoContribuicaoVM.TempoContribuicaoToModel(tempoContribuicaoVM));
                else calculadoraBusiness.AddTempoContribuicao(tempoContribuicaoVM.TempoContribuicaoToModel(tempoContribuicaoVM));

                SetSessionCalculadoraViewModel(model);
            }
            return Json(new { success });
        }

        // POST: Calculadora/ResultCalculadora
        [HttpPost, ActionName("ResultCalculadora")]
        [ValidateAntiForgeryToken]
        public ActionResult ResultCalculadora()
        {
            CalculadoraViewModel model = GetSessionCalculadoraViewModel();
            model = calculadoraBusiness.RealizaCalculo(model);
            return View(model);
        }

        [HttpPost]
        public PartialViewResult GetClientesAjax(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                CalculadoraViewModel model = GetSessionCalculadoraViewModel();

                ViewBag.CurrentSort = sortOrder;

                ViewBag.CurrentFilter = searchString;

                if (page == null) page = 1;
                model.Clientes = clienteBusiness.GetClientes(sortOrder, currentFilter, searchString, page).ToList();
                ViewBag.Clientes = model.Clientes.AsQueryable().ToPagedList((int)page, 3);

                SetSessionCalculadoraViewModel(model);
                return PartialView("_SelectCliente", ViewBag.Clientes);
            }
            catch (Exception)
            {

                throw;
            }



        }

        #region Session

        private void SetSessionCalculadoraViewModel(CalculadoraViewModel model)
        {
            var calculadoraViewModel = JsonConvert.SerializeObject(model);
            HttpContext.Session.SetString("calculadora", calculadoraViewModel);
        }

        private CalculadoraViewModel GetSessionCalculadoraViewModel()
        {
            var str = HttpContext.Session.GetString("calculadora");
            return JsonConvert.DeserializeObject<CalculadoraViewModel>(str);
        }
        #endregion

    }
}
