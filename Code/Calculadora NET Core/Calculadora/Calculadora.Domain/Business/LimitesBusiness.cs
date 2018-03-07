using System;
using System.Linq;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using Calculadora.DAL.Repository;

namespace Calculadora.Domain.Business
{
    public class LimitesBusiness : BaseBusiness
    {
        LimitesRepository limiteRepository;

        public LimitesBusiness(VanguardaContext context)
        {
            limiteRepository = new LimitesRepository(context);
        }

        public IQueryable<Limite> GetLimites(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                return limiteRepository.GetLimites(sortOrder, currentFilter, searchString, page);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Limite GetLimiteId(int? id)
        {
            try
            {
                return limiteRepository.GetLimiteId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SetLimite(Limite limite)
        {
            try
            {
                limiteRepository.SetLimite(limite);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateLimite(Limite limite)
        {
            try
            {
                limiteRepository.UpdateLimite(limite);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}

