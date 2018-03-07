using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using PagedList.Core;
using Calculadora.Domain.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Calculadora.Web.Controllers
{
    public class LimitesController : Controller
    {
        LimitesBusiness limitesBusiness;

        public LimitesController(VanguardaContext context)
        {
            limitesBusiness = new LimitesBusiness(context);
        }

        // GET: Limites
        public ActionResult IndexLimite(string sortOrder, string currentFilter, string searchString, int? page)
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
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IQueryable<Limite> limites = limitesBusiness.GetLimites(sortOrder, currentFilter, searchString, page);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(limites.ToPagedList(pageNumber, pageSize));
        }

        // GET: Limites/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Limite limite = db.Limites.Find(id);
            //if (limite == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(limite);
            return View();
        }

        // GET: Limites/Create
        public ActionResult CreateLimite()
        {
            return View();
        }

        // POST: Limites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLimite([Bind("LimiteID,Data,TetoContribuicao,TetoRMI,MenorValorTeto,MaiorValorTeto")] Limite limite)
        {
            if (ModelState.IsValid)
            {
                limitesBusiness.SetLimite(limite);
                return RedirectToAction("IndexLimite");
            }

            return View(limite);
        }

        [HttpPost]
        public JsonResult SaveLimiteAjax(string limiteId, string data, string tetoContribuicao, string tetoRmi, string menorValorTeto, string maiorValorTeto)
        {
            bool sucess = false;
            string error = "";
            if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(tetoContribuicao) && !string.IsNullOrEmpty(tetoRmi)
                 && !string.IsNullOrEmpty(menorValorTeto) && !string.IsNullOrEmpty(maiorValorTeto))
            {
                Limite limite = null;
                if (!string.IsNullOrEmpty(limiteId))
                {
                    limite = limitesBusiness.GetLimiteId(Int32.Parse(limiteId));
                    if (limite == null)
                    {
                        sucess = false;
                        error = "Limite não encontrado";
                        return Json(new { sucess, error });
                    }
                    //db.Entry(limite).State = EntityState.Modified;
                }
                else
                {
                    limite = new Limite();
                }

                limite.Data = DateTime.ParseExact(data, "MM/yyyy", null);
                double pvalor;
                double.TryParse(tetoContribuicao, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                limite.TetoContribuicao = pvalor;

                double.TryParse(tetoRmi, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                limite.TetoRMI = pvalor;

                double.TryParse(menorValorTeto, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                limite.MenorValorTeto = pvalor;

                double.TryParse(maiorValorTeto, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                limite.MaiorValorTeto = pvalor;

                if (string.IsNullOrEmpty(limiteId))
                {
                    limitesBusiness.SetLimite(limite);
                }
                else
                {
                    limitesBusiness.UpdateLimite(limite);
                }
                
                sucess = true;
            }
            else
            {
                sucess = false;
                error = "Preecher os campos corretamente";
            }

            return Json(new { sucess, error });
        }

        // GET: Limites/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Limite limite = db.Limites.Find(id);
            //if (limite == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(limite);
            return View();
        }

        // POST: Limites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("LimiteID,Data,TetoContribuicao,TetoRMI,MenorValorTeto,MaiorValorTeto")] Limite limite)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(limite).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(limite);
            return View();
        }

        // GET: Limites/Delete/5
        public ActionResult DeleteLimite(int? limiteId)
        {
            //if (limiteId == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Limite limite = db.Limites.Find(limiteId);
            //if (limite == null)
            //{
            //    return HttpNotFound();
            //}
            //else
            //{
            //    db.Limites.Remove(limite);
            //    db.SaveChanges();
            //}
            //return RedirectToAction("IndexLimite");
            return View();
        }

        // POST: Limites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Limite limite = db.Limites.Find(id);
            //db.Limites.Remove(limite);
            //db.SaveChanges();
            //return RedirectToAction("IndexLimite");
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
    }
}
