using System;
using System.Linq;
//using System.Data.Entity;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.DAL.Repository
{
    public class ValoresIndiceCorrecaoRepository : BaseRepository
    {
        private readonly VanguardaContext db;

        public ValoresIndiceCorrecaoRepository(VanguardaContext context)
        {
            db = context;
        }



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