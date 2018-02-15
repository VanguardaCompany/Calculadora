using System;
using System.Linq;

using CalculoPrev.DAL.Models;
using CalculoPrev.DAL.Repository;

namespace CalculoPrev.Domain.Business
{
    public class LimitesBusiness : BaseBusiness
    {
        LimitesRepository limiteRepository = new LimitesRepository();

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

