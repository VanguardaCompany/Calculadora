﻿using System;
using System.Linq;
using CalculoPrev.DAL.Models;
using CalculoPrev.DAL.Repository;

namespace CalculoPrev.Domain.Business
{
    public class IndicesCorrecaoBusiness : BaseBusiness
    {
        IndicesCorrecaoRepository indicesRepository = new IndicesCorrecaoRepository();

        public IQueryable<IndiceCorrecao> GetIndices(string sortOrder, bool percentagemFilter, bool valorFilter, bool aniversarioFilter, bool somatorioFilter, bool mensaisFilter, bool diariosFilter, bool atualizadosFilter, bool desatualizadosFilter, bool extintosFilter, string searchString, int? page)
        {
            try
            {
                return indicesRepository.GetIndices(sortOrder, percentagemFilter, valorFilter, aniversarioFilter, somatorioFilter, mensaisFilter, diariosFilter, atualizadosFilter, desatualizadosFilter, extintosFilter, searchString, page);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IndiceCorrecao GetIndiceId(int? id)
        {
            try
            {
                return indicesRepository.GetIndiceId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SetIndice(IndiceCorrecao indice)
        {
            try
            {
                indicesRepository.SetIndice(indice);
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
                indicesRepository.UpdateIndice(indice);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}