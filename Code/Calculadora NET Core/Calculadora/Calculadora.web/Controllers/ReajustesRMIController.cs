using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using PagedList.Core;
using Calculadora.Domain.Business;

namespace Calculadora.Web.Controllers
{
    public class ReajustesRMIController : Controller
    {
        ReajustesRMIBusiness reajusteBusiness;

        public ReajustesRMIController(VanguardaContext context)
        {
            reajusteBusiness = new ReajustesRMIBusiness(context);
        }

        // GET: ReajusteRMIs
        public ActionResult IndexRMI(string sortOrder, string currentFilter, string searchString, int? page)
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

            IQueryable<ReajusteRMI> reajustes = reajusteBusiness.GetReajustes(sortOrder, currentFilter, searchString, page);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(reajustes.ToPagedList(pageNumber, pageSize));
        }

        // GET: ReajusteRMIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest); 
            }
            ReajusteRMI reajusteRMI = reajusteBusiness.GetReajusteId(id);
            if (reajusteRMI == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(reajusteRMI);
        }

       
        // POST: ReajusteRMIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRMI([Bind("ReajusteRMIID,Data,ReajusteTotal,fator1,fator2,fator3,fator4,fator5,fator6,fator7,fator8,fator9,fator10,fator11,fator12,linkPrevidencia")] ReajusteRMI reajusteRMI)
        {
            if (ModelState.IsValid)
            {
                reajusteBusiness.SetReajuste(reajusteRMI);
                return RedirectToAction("IndexRMI");
            }

            //return View(reajusteRMI);
            return View();
        }

        // GET: ReajusteRMIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            ReajusteRMI reajusteRMI = reajusteBusiness.GetReajusteId(id);
            if (reajusteRMI == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(reajusteRMI);
        }

        // POST: ReajusteRMIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRMI([Bind("ReajusteRMIID,Data,ReajusteTotal,fator1,fator2,fator3,fator4,fator5,fator6,fator7,fator8,fator9,fator10,fator11,fator12,linkPrevidencia")] ReajusteRMI reajusteRMI)
        {
            if (ModelState.IsValid)
            {
                reajusteBusiness.UpdateReajuste(reajusteRMI);
                return RedirectToAction("IndexRMI");
            }
            return View(reajusteRMI);
        }

        // GET: ReajusteRMIs/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //ReajusteRMI reajusteRMI = db.ReajusteRMIs.Find(id);
            //if (reajusteRMI == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(reajusteRMI);
            return View();
        }

        // POST: ReajusteRMIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //ReajusteRMI reajusteRMI = db.ReajusteRMIs.Find(id);
            //db.ReajusteRMIs.Remove(reajusteRMI);
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

        
        public JsonResult GetReajusteAjax(string reajusteId)
        {
            bool sucess = false;
            string error = "";
            if (!string.IsNullOrEmpty(reajusteId))
            {
                ReajusteRMI reajuste = reajusteBusiness.GetReajusteId(Int32.Parse(reajusteId));
                if (reajuste == null)
                {
                    sucess = false;
                    error = "Reajuste não encontrado";
                    return Json(new { sucess, error });
                }
                sucess = true;
                return Json(new { sucess, error, reajuste });
            }
            else
            {
                sucess = false;
                error = "Id vazio";
                return Json(new { sucess, error });
            }
        }


        [HttpPost]
        public JsonResult SaveReajusteAjax(string reajusteId, string data, string reajusteTotal, string fator1, string fator2, string fator3, string fator4, string fator5, string fator6, string fator7, string fator8, string fator9, string fator10, string fator11, string fator12, string link)
        {
            bool sucess = false;
            string error = "";
            if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(reajusteTotal))
            {
                ReajusteRMI reajuste = null;
                if (!string.IsNullOrEmpty(reajusteId))
                {
                    reajuste = reajusteBusiness.GetReajusteId(Int32.Parse(reajusteId));
                    
                    if (reajuste == null)
                    {
                        sucess = false;
                        error = "Reajuste não encontrado";
                        return Json(new { sucess, error });
                    }
                    //db.Entry(reajuste).State = EntityState.Modified;
                }
                else
                {
                    reajuste = new ReajusteRMI();
                }

                reajuste.Data = DateTime.ParseExact(data, "MM/yyyy", null);
                double pvalor;
                double.TryParse(reajusteTotal, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.ReajusteTotal = pvalor;

                double.TryParse(fator1, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator1 = pvalor;

                double.TryParse(fator2, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator2 = pvalor;

                double.TryParse(fator3, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator3 = pvalor;

                double.TryParse(fator4, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator4 = pvalor;

                double.TryParse(fator5, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator5 = pvalor;
                double.TryParse(fator6, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator6 = pvalor;
                double.TryParse(fator7, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator7 = pvalor;
                double.TryParse(fator8, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator8 = pvalor;
                double.TryParse(fator9, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator9 = pvalor;
                double.TryParse(fator10, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator10 = pvalor;
                double.TryParse(fator11, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator11 = pvalor;
                double.TryParse(fator12, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out pvalor); //valor;
                reajuste.Fator12 = pvalor;
                reajuste.linkPrevidencia = link;

                if (string.IsNullOrEmpty(reajusteId))
                {
                    reajusteBusiness.SetReajuste(reajuste);
                }
                else
                {
                    reajusteBusiness.UpdateReajuste(reajuste);
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
    }
}
