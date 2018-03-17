using System;
using System.Linq;
using Calculadora.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.DAL.Repository
{
    public class ClientesRepository : BaseRepository
    {
        private readonly VanguardaContext db;

        public ClientesRepository(VanguardaContext context)
        {
            db = context;
        }

        public IQueryable<Cliente> GetClientes(string sortOrder, string currentFilter, string searchString, int? page)
        {
            IQueryable<Cliente> clientes = from s in db.Clientes
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(s => s.Nome.Contains(searchString)
                                       || s.Cpf.Contains(searchString));
            }
            switch (sortOrder)
            {
                //case "name_desc":
                //    indicesCorrecao = indicesCorrecao.OrderByDescending(s => s.Nome);
                //    break;

                default:  // Name ascending 
                    clientes = clientes.OrderBy(s => s.Nome);
                    break;
            }

            return clientes;
        }

        public IQueryable<Cliente> GetAllClientes()
        {
            IQueryable<Cliente> clientes = from s in db.Clientes
                                           select s;
            

            return clientes;
        }

        public Cliente GetClienteId(int? id)
        {
            Cliente cliente = (Cliente)db.Pessoas.Find(id);
            if (cliente == null)
            {
                return null;
            }
            cliente.Endereco = db.Enderecos.Find(cliente.EnderecoID);
            cliente.Escritorio = db.Escritorios.Find(cliente.EscritorioID);

            return cliente;
        }

        public void SetCliente(Cliente cliente)
        {
            try
            {
                //db.Enderecos.Add(cliente.Endereco);
                cliente.EnderecoID = 3;
                cliente.EscritorioID = 1;
                db.Pessoas.Add(cliente);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateCliente(Cliente cliente)
        {
            try
            {
                //db.Entry(cliente.Endereco).State = EntityState.Modified;
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}