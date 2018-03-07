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
    public class LimitesRepository : BaseRepository
    {
        private readonly VanguardaContext db;

        public LimitesRepository(VanguardaContext context)
        {
            db = context;
        }

        public IQueryable<Limite> GetLimites(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                var limites = from s in db.Limites
                              select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    limites = limites.Where(s => s.Data.Equals(searchString));
                }
                switch (sortOrder)
                {
                    //case "name_desc":
                    //    indicesCorrecao = indicesCorrecao.OrderByDescending(s => s.Nome);
                    //    break;

                    default:  // Name ascending 
                        limites = limites.OrderBy(s => s.Data);
                        break;
                }

                return limites;
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
                Limite limite = db.Limites.Find(id);
                if (limite == null)
                {
                    return null;
                }
                return limite;
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
                db.Limites.Add(limite);
                db.SaveChanges();
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
                db.Entry(limite).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}