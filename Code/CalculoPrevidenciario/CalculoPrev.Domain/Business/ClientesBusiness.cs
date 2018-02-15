using System;
using System.Linq;
using CalculoPrev.DAL.Models;
using CalculoPrev.DAL.Repository;

namespace CalculoPrev.Domain.Business
{
    public class ClientesBusiness : BaseBusiness
    {
        ClientesRepository clientesRepository = new ClientesRepository();

        public IQueryable<Cliente> GetClientes(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                return clientesRepository.GetClientes(sortOrder, currentFilter, searchString, page);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Cliente GetClienteId(int? id)
        {
            try
            {
                return clientesRepository.GetClienteId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SetCliente(Cliente cliente)
        {
            try
            {
                clientesRepository.SetCliente(cliente);
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
                clientesRepository.UpdateCliente(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}