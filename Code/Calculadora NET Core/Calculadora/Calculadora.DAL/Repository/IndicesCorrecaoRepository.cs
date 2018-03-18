using System;
using System.Linq;
//using System.Data.Entity;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.DAL.Repository
{
    public class IndicesCorrecaoRepository : BaseRepository
    {
        private readonly VanguardaContext db;

        public IndicesCorrecaoRepository(VanguardaContext context)
        {
            db = context;
        }

        public IQueryable<IndiceCorrecao> GetIndices(string sortOrder, bool percentagemFilter, bool valorFilter, bool aniversarioFilter, bool somatorioFilter, bool mensaisFilter, bool diariosFilter, bool atualizadosFilter, bool desatualizadosFilter, bool extintosFilter, string searchString, int? page)
        {
            var indices = from s in db.IndicesCorrecao
                                  select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                indices = indices.Where(s =>
                                    String.IsNullOrEmpty(searchString) ? true : (s.Nome.Contains(searchString)
                                    || s.Descricao.Contains(searchString)
                                    || s.Informacoes.Contains(searchString))
                                    );
            }
            indices = indices.Where(s =>
                                    ((percentagemFilter && s.Tipo == EnumTipoIC.Percentagem)
                                    || (valorFilter && s.Tipo == EnumTipoIC.Valor)
                                    || (aniversarioFilter && s.Tipo == EnumTipoIC.DataAniversario)
                                    || (somatorioFilter && s.Tipo == EnumTipoIC.Somatorio))
                                    && ((mensaisFilter && s.Periodicidade == EnumPeriodicidadeIC.Mensal)
                                    || (diariosFilter && s.Periodicidade == EnumPeriodicidadeIC.Diario)
                                       //&& atualizadosFilter ? s.Tipo == EnumTipoIC.Valor : true
                                       //&& desatualizadosFilter ? s.Tipo == EnumTipoIC.Valor : true
                                       //&& extintosFilter ? s.Tipo == EnumTipoIC.Valor : true
                                       )).Include(v=>v.Valores);

            switch (sortOrder)
            {
                //case "name_desc":
                //    indicesCorrecao = indicesCorrecao.OrderByDescending(s => s.Nome);
                //    break;

                default:  // Name ascending 
                    indices = indices.OrderBy(s => s.Nome);
                    break;
            }

            return indices;
        }

        public IndiceCorrecao GetIndiceId(int? id)
        {
            IndiceCorrecao indice = db.IndicesCorrecao.Include(v=>v.Valores).Where(i=>i.IndiceCorrecaoID==id).FirstOrDefault();
            if (indice == null)
            {
                return null;
            }
            return indice;
        }

        public void SetIndice(IndiceCorrecao indice)
        {
            try
            {
                db.IndicesCorrecao.Add(indice);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateIndice(IndiceCorrecao indice)
        {
            try
            {
                db.Entry(indice).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}