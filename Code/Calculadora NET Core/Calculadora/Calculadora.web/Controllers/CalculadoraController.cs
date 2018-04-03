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
using Calculadora.web.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Calculadora.web.Services;
using System.Threading.Tasks;
using WkWrap.Core;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Calculadora.Web.Controllers
{
    public class CalculadoraController : Controller
    {
        ClientesBusiness clienteBusiness;

        CalculadoraBusiness calculadoraBusiness;

        private IHostingEnvironment _env;

        private readonly IViewRenderService _viewRenderService;


        public CalculadoraController(VanguardaContext context, IHostingEnvironment env, IViewRenderService viewRenderService)
        {
            _env = env;
            _viewRenderService = viewRenderService;
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
            model.ClienteSelecionado = clienteBusiness.GetClienteId(idCliente, false);
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
        public PartialViewResult AddSimulacaoAjax(string nome)
        {
            try
            {
                CalculadoraViewModel model = GetSessionCalculadoraViewModel();

                SimulacaoViewModel simulacaoVM = new SimulacaoViewModel();
                simulacaoVM.Data = DateTime.Now;
                simulacaoVM.Cliente = model.ClienteSelecionado;
                simulacaoVM.Nome = nome;

                simulacaoVM.SimulacaoID = calculadoraBusiness.AddSimulacao(simulacaoVM.SimulacaoToModel(simulacaoVM));

                model.SimulacaoSelecionada = simulacaoVM;

                model.Simulacoes.Add(model.SimulacaoSelecionada);

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

        //private RelatorioDuplicata getRelatorio()
        //{
        //    var rpt = new RelatorioDuplicata();

        //    //var webRoot = _env.WebRootPath;
        //    //var file = System.IO.Path.Combine(webRoot, "test.txt");
        //    //System.IO.File.WriteAllText(file, "Hello World!");

        //    //rpt.BasePath = Server.MapPath("/");
        //    rpt.BasePath = _env.WebRootPath;

        //    rpt.PageTitle = "Relatório de Duplicatas";
        //    rpt.PageTitle = "Relatório de Duplicatas";
        //    rpt.ImprimirCabecalhoPadrao = true;
        //    rpt.ImprimirRodapePadrao = true;

        //    return rpt;
        //}

        //public ActionResult Preview()
        //{
        //    var rpt = getRelatorio();

        //    return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        //}

        //public FileResult BaixarPDF()
        //{
        //    var rpt = getRelatorio();

        //    return File(rpt.GetOutput().GetBuffer(), "application/pdf", "Documento.pdf");
        //}

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




        public async Task<IActionResult> DownloadPdf()
        {
            CalculadoraViewModel model = GetSessionCalculadoraViewModel();
            model = calculadoraBusiness.RealizaCalculo(model);

            var result = await _viewRenderService.RenderToStringAsync("Calculadora/ResultCalculo", model);

            string htmlContent = result;
            var wkhtmltopdf = new FileInfo(@"C:\Users\rerum\Documents\GitHub\Calculadora\Code\Calculadora NET Core\Calculadora\Calculadora.web\wwwroot\Rotativa\wkhtmltopdf.exe");
            var converter = new HtmlToPdfConverter(wkhtmltopdf);
            var pdfBytes = converter.ConvertToPdf(htmlContent);

            FileResult fileResult = new FileContentResult(pdfBytes, "application/pdf");
            fileResult.FileDownloadName = "ResultadoCalculo.pdf";
            return fileResult;
        }
        #endregion

    }
}
