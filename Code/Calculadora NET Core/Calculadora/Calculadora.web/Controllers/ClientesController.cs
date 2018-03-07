using System;
using System.Linq;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using PagedList.Core;
using Calculadora.Domain.Business;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Calculadora.web.Models;

namespace Calculadora.Web.Controllers
{
    
    public class ClientesController : Controller
    {
        ClientesBusiness clienteBusiness;

        public ClientesController(VanguardaContext context)
        {
            //_context = context;
            clienteBusiness = new ClientesBusiness(context);
        }

        // GET: Clientes
        public IActionResult IndexCliente(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IQueryable<Cliente> clientes = clienteBusiness.GetClientes(sortOrder, currentFilter, searchString, page);

            
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(clientes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Cliente cliente = (Cliente)db.Pessoas.Find(id);
            //if (cliente == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cliente);
            return View();
        }

        [HttpGet]
        // GET: Clientes/Create
        public ViewResult CreateCliente()
        {
            //ViewBag.EscritorioID = new SelectList(db.Escritorios, "EscritorioID", "Nome");
            Cliente model = new Cliente();
            //model.Endereco = new Endereco();
            model.Nascimento = DateTime.Now;
            
            return View(model);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    clienteBusiness.SetCliente(cliente);
                    //return RedirectToAction(nameof(Index));
                    return RedirectToAction("IndexCliente");
                }
                catch (Exception)
                {

                    throw;
                }
               
            }

            //ViewBag.EscritorioID = new SelectList(db.Escritorios, "EscritorioID", "Nome", cliente.EscritorioID);
            return View();
        }

        // GET: Clientes/Edit/5
        public IActionResult EditCliente(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);//new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = clienteBusiness.GetClienteId(id);

            if (cliente == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);// HttpNotFound();
            }
            
            //ViewBag.EscritorioID = new SelectList(db.Escritorios, "EscritorioID", "Nome", cliente.EscritorioID);
            return View(cliente);
            
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                clienteBusiness.UpdateCliente(cliente);
                return RedirectToAction("IndexCliente");
            }
            //ViewBag.EscritorioID = new SelectList(db.Escritorios, "EscritorioID", "Nome", cliente.EscritorioID);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult DeleteCliente(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Cliente cliente = (Cliente)db.Pessoas.Find(id);
            //if (cliente == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(cliente);
            return View();
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Cliente cliente = (Cliente)db.Pessoas.Find(id);
            //db.Pessoas.Remove(cliente);
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
