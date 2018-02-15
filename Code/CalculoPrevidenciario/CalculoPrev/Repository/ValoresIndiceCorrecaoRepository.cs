using System;
using System.Linq;
using System.Data.Entity;
using CalculoPrev.DAL.DAL;
using CalculoPrev.DAL.Models;

namespace CalculoPrev.DAL.Repository
{
    public class ValoresIndiceCorrecaoRepository : BaseRepository
    {
        private static Vanguardacontext db = new Vanguardacontext();

        

        public void SetValor(ValorIndiceCorrecao valor)
        {
            try
            {
                db.ValoresIndiceCorrecao.Add(valor);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateValor(ValorIndiceCorrecao valor)
        {
            try
            {
                db.Entry(valor).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}