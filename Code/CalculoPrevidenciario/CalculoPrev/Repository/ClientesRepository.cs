using System;
using System.Linq;
using System.Data.Entity;
using CalculoPrev.DAL.DAL;
using CalculoPrev.DAL.Models;

namespace CalculoPrev.DAL.Repository
{
    public class ClientesRepository : BaseRepository
    {
        private static Vanguardacontext db = new Vanguardacontext();

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

        public Cliente GetClienteId(int? id)
        {
            Cliente cliente = (Cliente)db.Pessoas.Find(id);
            if (cliente == null)
            {
                return null;
            }
            cliente.Endereco = db.Enderecos.Find(cliente.EnderecoID);

            return cliente;
        }

        public void SetCliente(Cliente cliente)
        {
            try
            {
                db.Enderecos.Add(cliente.Endereco);
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
                db.Entry(cliente.Endereco).State = EntityState.Modified;
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