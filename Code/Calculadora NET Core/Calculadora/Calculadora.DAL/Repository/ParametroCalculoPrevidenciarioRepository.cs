using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace Calculadora.DAL.Repository
{
    public class ParametroCalculoPrevidenciarioRepository : BaseRepository
    {
        private readonly VanguardaContext db;

        public ParametroCalculoPrevidenciarioRepository(VanguardaContext context)
        {
            db = context;
        }

        public IQueryable<ParametroCalculoPrevidenciario> GetParametrosCalculoPrevidenciario(string searchString)
        {
            try
            {
                var parametros = from s in db.ParametroCalculoPrevidenciario
                              select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    parametros = parametros.Where(s => s.TipoParametro.Equals(searchString));
                }

                return parametros;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ParametroCalculoPrevidenciario GetParametroCalculoPrevidenciarioById(int? id)
        {
            try
            {
                ParametroCalculoPrevidenciario parametro = db.ParametroCalculoPrevidenciario.Find(id);
                if (parametro == null)
                {
                    return null;
                }
                return parametro;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SetParametroCalculoPrevidenciario(ParametroCalculoPrevidenciario parametro)
        {
            try
            {
                db.ParametroCalculoPrevidenciario.Add(parametro);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void UpdateParametroCalculoPrevidenciario(ParametroCalculoPrevidenciario parametro)
        {
            try
            {
                db.Entry(parametro).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}