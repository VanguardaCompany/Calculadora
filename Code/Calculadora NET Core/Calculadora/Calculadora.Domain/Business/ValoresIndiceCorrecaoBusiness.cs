using System;
using System.Linq;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using Calculadora.DAL.Repository;

namespace Calculadora.Domain.Business
{
    public class ValoresIndiceCorrecaoBusiness : BaseBusiness
    {
        ValoresIndiceCorrecaoRepository valorRepository;

        public ValoresIndiceCorrecaoBusiness(VanguardaContext context)
        {
            valorRepository = new ValoresIndiceCorrecaoRepository(context);
        }

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
