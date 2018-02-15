using System;
using System.Linq;
using CalculoPrev.DAL.Models;
using CalculoPrev.DAL.Repository;

namespace CalculoPrev.Domain.Business
{
    public class ValoresIndiceCorrecaoBusiness : BaseBusiness
    {
        ValoresIndiceCorrecaoRepository valorRepository = new ValoresIndiceCorrecaoRepository();
        

        public void SetIndice(ValorIndiceCorrecao valor)
        {
            try
            {
                valorRepository.SetValor(valor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateIndice(ValorIndiceCorrecao valor)
        {
            try
            {
                valorRepository.UpdateValor(valor);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
