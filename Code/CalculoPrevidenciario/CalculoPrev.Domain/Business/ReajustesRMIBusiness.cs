using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CalculoPrev.DAL.Models;
using CalculoPrev.DAL.Repository;

namespace CalculoPrev.Domain.Business
{
    public class ReajustesRMIBusiness : BaseBusiness
    {
        ReajustesRMIRepository reajustesRepository = new ReajustesRMIRepository();

        public IQueryable<ReajusteRMI> GetReajustes(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                return reajustesRepository.GetReajustes(sortOrder, currentFilter, searchString, page);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ReajusteRMI GetReajusteId(int? id)
        {
            try
            {
                return reajustesRepository.GetReajusteId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SetReajuste(ReajusteRMI reajuste)
        {
            try
            {
                reajustesRepository.SetReajuste(reajuste);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateReajuste(ReajusteRMI reajuste)
        {
            try
            {
                reajustesRepository.UpdateReajuste(reajuste);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
