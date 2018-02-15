using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalculoPrev.DAL.Models;

namespace CalculoPrev.Web.Controllers
{
    public class ValoresIndiceCorrecaoController : Controller
    {
        // GET: ValoresIndiceCorrecao
        public ActionResult Index()
        {
            //var valorIndiceCorrecaos = db.ValoresIndiceCorrecao.Include(v => v.IndiceCorrecao);
            //return View(valorIndiceCorrecaos.ToList());
            return View();
        }

        // GET: ValoresIndiceCorrecao/Details/5
        public ActionResult Details(DateTime id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //ValorIndiceCorrecao valorIndiceCorrecao = db.ValoresIndiceCorrecao.Find(id);
            //if (valorIndiceCorrecao == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(valorIndiceCorrecao);
            return View();
        }

        // GET: ValoresIndiceCorrecao/Create
        public ActionResult Create()
        {
            //ViewBag.IndiceCorrecaoID = new SelectList(db.IndicesCorrecao, "IndiceCorrecaoID", "Nome");
            //return View();
            return View();
        }

        // POST: ValoresIndiceCorrecao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Data,IndiceCorrecaoID,Valor")] ValorIndiceCorrecao valorIndiceCorrecao)
        {
            //if (ModelState.IsValid)
            //{
            //    db.ValoresIndiceCorrecao.Add(valorIndiceCorrecao);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.IndiceCorrecaoID = new SelectList(db.IndicesCorrecao, "IndiceCorrecaoID", "Nome", valorIndiceCorrecao.IndiceCorrecaoID);
            //return View(valorIndiceCorrecao);
            return View();
        }

        // GET: ValoresIndiceCorrecao/Edit/5
        public ActionResult Edit(DateTime id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //ValorIndiceCorrecao valorIndiceCorrecao = db.ValoresIndiceCorrecao.Find(id);
            //if (valorIndiceCorrecao == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.IndiceCorrecaoID = new SelectList(db.IndicesCorrecao, "IndiceCorrecaoID", "Nome", valorIndiceCorrecao.IndiceCorrecaoID);
            //return View(valorIndiceCorrecao);
            return View();
        }

        // POST: ValoresIndiceCorrecao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Data,IndiceCorrecaoID,Valor")] ValorIndiceCorrecao valorIndiceCorrecao)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(valorIndiceCorrecao).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.IndiceCorrecaoID = new SelectList(db.IndicesCorrecao, "IndiceCorrecaoID", "Nome", valorIndiceCorrecao.IndiceCorrecaoID);
            //return View(valorIndiceCorrecao);
            return View();
        }

        // GET: ValoresIndiceCorrecao/Delete/5
        public ActionResult Delete(DateTime id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //ValorIndiceCorrecao valorIndiceCorrecao = db.ValoresIndiceCorrecao.Find(id);
            //if (valorIndiceCorrecao == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(valorIndiceCorrecao);
            return View();
        }

        // POST: ValoresIndiceCorrecao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            //ValorIndiceCorrecao valorIndiceCorrecao = db.ValoresIndiceCorrecao.Find(id);
            //db.ValoresIndiceCorrecao.Remove(valorIndiceCorrecao);
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
    }
}
