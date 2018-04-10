using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Calculadora.Domain.Business;
using Calculadora.DAL;
using Calculadora.Domain.Models;
using Newtonsoft.Json;
using PagedList.Core;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Calculadora.web.Services;
using System.Threading.Tasks;
using WkWrap.Core;
using iTextSharp.text.pdf;
using System.Text;
using Calculadora.DAL.Models;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

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
                List<TempoContribuicaoViewModel> temposOcioso = new List<TempoContribuicaoViewModel>();
                TempoContribuicaoViewModel tempoOcioso = new TempoContribuicaoViewModel();
                foreach (var item in model.TempoContribuicoes)
                {
                    if (tempoOcioso.DataDemissao != null && tempoOcioso.DataDemissao != DateTime.MinValue)
                    {
                        if (tempoOcioso.DataDemissao < item.DataAdmissao)
                        {
                            tempoOcioso.DataAdmissao = tempoOcioso.DataDemissao;
                            tempoOcioso.DataDemissao = item.DataAdmissao;
                            tempoOcioso.Empregador = "TEMPO OCIOSO";
                            tempoOcioso.TempoOcioso = true;
                            temposOcioso.Add(tempoOcioso);
                            tempoOcioso = new TempoContribuicaoViewModel();
                            tempoOcioso.DataDemissao = item.DataDemissao;
                        }
                        else
                        {
                            tempoOcioso.DataDemissao = item.DataDemissao;
                        }
                    }
                    else
                    {
                        tempoOcioso.DataDemissao = item.DataDemissao;
                    }
                }
                foreach (var item in temposOcioso)
                {
                    model.TempoContribuicoes.Add(item);
                }
                model.TempoContribuicoes = model.TempoContribuicoes.OrderBy(s => s.DataAdmissao).ToList();
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

        [HttpPost]
        public ActionResult DeleteTempoContribuicaoAjax(int id)
        {
            string success = "true";
            try
            {
                calculadoraBusiness.DeleteTempoContribuicao(id);
            }
            catch (Exception)
            {
                success = "false";
                throw;
            }

            return Json(new { success });
        }

        // POST: Calculadora/ResultCalculadora
        [HttpPost, ActionName("ResultCalculadora")]
        [ValidateAntiForgeryToken]
        public ActionResult ResultCalculadora()
        {
            //ViewBag.Layout = "_PrintLayout";

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

        [HttpPost]
        public ActionResult UploadFileAjax(IFormFile file)
        {
            CalculadoraViewModel model = GetSessionCalculadoraViewModel();

            string success = "true";

            var result = string.Empty;

            foreach (TempoContribuicao tempo in ExtrairVinculosTrabalhistasCNIS(file.OpenReadStream()))
            {
                tempo.SimulacaoID = model.SimulacaoSelecionada.SimulacaoID;
                calculadoraBusiness.AddTempoContribuicao(tempo);
            }

            SetSessionCalculadoraViewModel(model);

            return Json(new { success });
            //return PartialView("_TempoContribuicoes", model);
        }

        public static List<TempoContribuicao> ExtrairVinculosTrabalhistasCNIS(Stream file)//string file)//
        {
            string searchText = "Seq.";
            StringBuilder text = new StringBuilder();
            List<string> lines = new List<string>();
            List<string> listaVinculosPdf = new List<string>();
            List<TempoContribuicao> listaVinculos = new List<TempoContribuicao>();
            bool ehVinculo = false;

            try
            {
                using (PdfReader reader = new PdfReader(file))
                {
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(reader, i, strategy);
                        text.Append(currentText);
                    }
                }

                lines = text.ToString().Trim().Split('\n').ToList();
                foreach (string item in lines)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (ehVinculo && !item.ToUpper().Contains("Benefício".ToUpper()) && !item.ToUpper().Contains("Segurado Especial".ToUpper()))
                            listaVinculosPdf.Add(item);

                        ehVinculo = (item.ToUpper().Contains(searchText.ToUpper()));
                    }
                }

                foreach (string linha in listaVinculosPdf)
                {
                    DateTime dataInicio = DateTime.MinValue;
                    DateTime dataFim = DateTime.MinValue;
                    string nomeEmpregador = String.Empty;
                    string[] colunas = linha.Split(' ');
                    string padraoCnpj = @"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)";
                    int aux = -1;

                    foreach (string coluna in colunas)
                    {
                        if (int.TryParse(coluna, out aux) || (Regex.IsMatch(coluna, padraoCnpj))) continue;

                        if (dataInicio == DateTime.MinValue)
                        {
                            if (!DateTime.TryParse(coluna, out dataInicio))
                                nomeEmpregador = nomeEmpregador + " " + coluna;

                            continue;
                        }

                        //if (dataInicio == DateTime.MinValue && DateTime.TryParse(coluna, out dataInicio)) continue;

                        if (dataInicio != DateTime.MinValue && dataFim == DateTime.MinValue)
                            if (DateTime.TryParse(coluna, out dataFim)) break;
                    }

                    if (dataInicio != DateTime.MinValue && dataFim != DateTime.MinValue)
                        listaVinculos.Add(new TempoContribuicao() { DataAdmissao = dataInicio, DataDemissao = dataFim, Empregador = nomeEmpregador.TrimStart() });
                }

                return listaVinculos;
            }
            catch (Exception ex)
            {
                throw ex;
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
