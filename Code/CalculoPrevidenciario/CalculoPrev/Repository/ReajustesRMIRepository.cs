using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalculoPrev.DAL.DAL;
using CalculoPrev.DAL.Models;
using System.Data.Entity;

namespace CalculoPrev.DAL.Repository
{
    public class ReajustesRMIRepository : BaseRepository
    {
        private static Vanguardacontext db = new Vanguardacontext();

        public IQueryable<ReajusteRMI> GetReajustes(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                IQueryable<ReajusteRMI> reajustes = from s in db.ReajusteRMIs
                                                    select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    reajustes = reajustes.Where(s => s.Data.Equals(searchString));
                }
                switch (sortOrder)
                {
                    //case "name_desc":
                    //    indicesCorrecao = indicesCorrecao.OrderByDescending(s => s.Nome);
                    //    break;

                    default:  // Name ascending 
                        reajustes = reajustes.OrderByDescending(s => s.Data);
                        break;
                }

                return reajustes;
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
                ReajusteRMI reajuste = db.ReajusteRMIs.Find(id);
                if (reajuste == null)
                {
                    return null;
                }
                return reajuste;
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
                db.ReajusteRMIs.Add(reajuste);
                db.SaveChanges();
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
                db.Entry(reajuste).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}