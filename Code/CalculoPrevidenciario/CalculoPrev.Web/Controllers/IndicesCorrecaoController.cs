using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalculoPrev.DAL.Models;
using PagedList;
using CalculoPrev.Domain.Business;

namespace CalculoPrev.Web.Controllers
{
    public class IndicesCorrecaoController : Controller
    {
        IndicesCorrecaoBusiness indiceBusiness = new IndicesCorrecaoBusiness();
        ValoresIndiceCorrecaoBusiness valorBusiness = new ValoresIndiceCorrecaoBusiness();

        // GET: IndicesCorrecao
        public ViewResult IndexIC(string sortOrder, bool percentagemFilter, bool valorFilter, bool aniversarioFilter, bool somatorioFilter, bool mensaisFilter, bool diariosFilter, bool atualizadosFilter, bool desatualizadosFilter, bool extintosFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                //searchString = currentFilter;
            }

            ViewBag.PercentagemFilter = percentagemFilter;
            ViewBag.ValorFilter = valorFilter;
            ViewBag.AniversarioFilter = aniversarioFilter;
            ViewBag.SomatorioFilter = somatorioFilter;
            ViewBag.MensaisFilter = mensaisFilter;
            ViewBag.DiariosFilter = diariosFilter;
            ViewBag.AtualizadosFilter = atualizadosFilter;
            ViewBag.DesatualizadosFilter = desatualizadosFilter;
            ViewBag.ExtintosFilter = extintosFilter;

            IQueryable<IndiceCorrecao> indices = indiceBusiness.GetIndices(sortOrder, percentagemFilter, valorFilter, aniversarioFilter, somatorioFilter, mensaisFilter, diariosFilter, atualizadosFilter, desatualizadosFilter, extintosFilter, searchString, page);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(indices.ToPagedList(pageNumber, pageSize));
        }

        // GET: IndicesCorrecao/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //IndiceCorrecao indiceCorrecao = db.IndicesCorrecao.Find(id);
            //if (indiceCorrecao == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // GET: IndicesCorrecao/Create
        public ActionResult CreateIC()
        {
            return View();
        }

        // POST: IndicesCorrecao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIC([Bind(Include = "IndiceCorrecaoID,Nome,Periodicidade,Tipo,DataInicio,DataFim,UltimoValor,Descricao,Estado,Informacoes,Extinto,Atualizado")] IndiceCorrecao indiceCorrecao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    indiceBusiness.SetIndice(indiceCorrecao);
                    return RedirectToAction("IndexIC", new { percentagemFilter = true, valorFilter = true, aniversarioFilter = true, somatorioFilter = true, mensaisFilter = true, diariosFilter = true, atualizadosFilter = true, desatualizadosFilter = true, extintosFilter = true });
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return View(indiceCorrecao);
        }

        [HttpPost]
        public JsonResult CreateValorICAjax(string data, string valor, int indiceCorrecaoID)
        {
            //bool sucess = false;
            //string error = "";
            //if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(valor) && (indiceCorrecaoID > 0))
            //{
            //    IndiceCorrecao ic = db.IndicesCorrecao.Find(indiceCorrecaoID);
            //    ValorIndiceCorrecao v = new ValorIndiceCorrecao();
            //    v.Data = DateTime.ParseExact(data, "MM/yyyy", null);
            //    double pvalor;
            //    double.TryParse(valor, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
            //    v.Valor = pvalor;
            //    ic.Valores.Add(v);
            //    db.SaveChanges();
            //    //return RedirectToAction("IndexIC");
            //    sucess = true;
            //}
            //else
            //{
            //    sucess = false;
            //    error = "Preecher os campos corretamente";
            //}


            //return Json(new {sucess, error}, JsonRequestBehavior.AllowGet);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        // GET: IndicesCorrecao/Edit/5
        public ActionResult EditIC(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IndiceCorrecao indice = indiceBusiness.GetIndiceId(id);

            if (indice == null)
            {
                return HttpNotFound();
            }
            return View(indice);
        }

        // POST: IndicesCorrecao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIC([Bind(Include = "IndiceCorrecaoID,Nome,Periodicidade,Tipo,DataInicio,DataFim,UltimoValor,Descricao,Estado,Extinto,Atualizado")] IndiceCorrecao indiceCorrecao)
        {
            if (ModelState.IsValid)
            {
                indiceBusiness.UpdateIndice(indiceCorrecao);
                return RedirectToAction("IndexIC");
            }
            return View(indiceCorrecao);
        }

        // GET: IndicesCorrecao/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //IndiceCorrecao indiceCorrecao = db.IndicesCorrecao.Find(id);
            //if (indiceCorrecao == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(indiceCorrecao);
            return View();
        }

        // POST: IndicesCorrecao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //IndiceCorrecao indiceCorrecao = db.IndicesCorrecao.Find(id);
            //db.IndicesCorrecao.Remove(indiceCorrecao);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult AdicionarValorICAjax(int indiceCorrecaoID, string data, double valor)
        {
            DateTime dateAux;
            dateAux = DateTime.ParseExact(data.Substring(0, 10), "MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).Date;

            //IndiceCorrecao indiceCorrecao = db.IndicesCorrecao.Find(indiceCorrecaoID);
            // db.IndicesCorrecao.Add()

            ValorIndiceCorrecao valorIC = new ValorIndiceCorrecao();
            valorIC.Data = dateAux;
            valorIC.Valor = valor;
            valorIC.IndiceCorrecaoID = indiceCorrecaoID;

            valorBusiness.SetIndice(valorIC);
            
            return null;
        }

        [HttpPost]
        public ActionResult ConsultaValoresICAjax(int indiceCorrecaoID, CalculoPrev.DAL.Models.DataTable.DataTableRequest dataTableRequest)
        {
            return Json(new CalculoPrev.DAL.Models.DataTable.DataTableResponse()
            {
                draw = dataTableRequest.draw,
                data = indiceBusiness.GetIndiceId(indiceCorrecaoID).Valores,
                recordsTotal = 0,//response.Result.TotalItens,
                recordsFiltered = 0,//response.Result.TotalItens
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
