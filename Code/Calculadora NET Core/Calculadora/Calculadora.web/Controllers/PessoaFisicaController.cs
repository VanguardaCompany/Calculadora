using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CalculoPrev.Web.Controllers
{
    public class PessoaFisicaController : Controller
    {
        // GET: PessoaFisica
        public ActionResult Index()
        {
            return View();
        }

        // GET: PessoaFisica/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PessoaFisica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PessoaFisica/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: PessoaFisica/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PessoaFisica/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaFisica/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaFisica/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
