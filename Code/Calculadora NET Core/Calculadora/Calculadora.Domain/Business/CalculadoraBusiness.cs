using System;
using System.Linq;
using Calculadora.DAL;
using Calculadora.DAL.Models;
using Calculadora.DAL.Repository;
using Calculadora.Domain.Models;
using Calculadora.Simulador.Models;
using Calculadora.Simulador;

namespace Calculadora.Domain.Business
{
    public class CalculadoraBusiness : BaseBusiness
    {
        CalculadoraRepository calculadoraRepository;

        public CalculadoraBusiness(VanguardaContext context)
        {
            calculadoraRepository = new CalculadoraRepository(context);
        }

        public IQueryable<Simulacao> GetSimulacoes(int idCliente)
        {
            try
            {
                return calculadoraRepository.GetSimulacoes(idCliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Simulacao GetSimulacaoId(int idSimulacao)
        {
            try
            {
                return calculadoraRepository.GetSimulacaoId(idSimulacao);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddSimulacao(Simulacao simulacao)
        {
            try
            {
                calculadoraRepository.AddSimulacao(simulacao);
                return simulacao.SimulacaoID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<TempoContribuicao> GetTempoContribuicoes(int idSimulacao)
        {
            try
            {
                return calculadoraRepository.GetTempoContribuicoes(idSimulacao);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddTempoContribuicao(TempoContribuicao tempoContribuicao)
        {
            try
            {
                calculadoraRepository.AddTempoContribuicao(tempoContribuicao);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateTempoContribuicao(TempoContribuicao tempoContribuicao)
        {
            try
            {
                calculadoraRepository.UpdateTempoContribuicao(tempoContribuicao);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Recebe a data de ínicio do cálculo, o tempo de contribuição e devolve a data em que completará 35 anos de tempo de contribuição.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="anos"></param>
        /// <param name="mesese"></param>
        /// <param name="dias"></param>
        /// <returns></returns>
        public static DateTime DataCompletaTempoContribuicao(DateTime data, int anos, int meses, int dias)
        {
            if (35 > anos)
            {
                data = data.AddYears(35);
                data = data.AddYears(-anos);
                data = data.AddMonths(-meses);
                data = data.AddDays(-dias);
            }
            return data;
        }

        /// <summary>
        /// devolve o tempo de serviço por extenso em anos, meses e dias
        /// </summary>
        /// <param name="idSimulacao"></param>
        /// <returns></returns>
        public SimulacaoViewModel CalculoSimulacaoId(int idSimulacao)
        {
            try
            {
                Simulacao simulacao = calculadoraRepository.GetSimulacaoId(idSimulacao);
                SimulacaoViewModel simulacaoVM = SimulacaoToViewModel(simulacao);
                int anos = 0, meses = 0, dias = 0;
                simulacaoVM.Anos = 0;
                simulacaoVM.Meses = 0;
                simulacaoVM.Dias = 0;
                foreach (var item in simulacao.TempoContribuicoes)
                {
                    CalculaTempo(item.DataAdmissao, item.DataDemissao, ref anos, ref meses, ref dias);
                    simulacaoVM.Anos += anos;
                    simulacaoVM.Meses += meses;
                    simulacaoVM.Dias += dias;
                }
                simulacaoVM.TempoTotalContribuicao = TempoPorExtenso(simulacaoVM.Anos, simulacaoVM.Meses, simulacaoVM.Dias);

                simulacaoVM.DataCompletaTempoContribuicao = DataCompletaTempoContribuicao(DateTime.Now, simulacaoVM.Anos, simulacaoVM.Meses, simulacaoVM.Dias);

                return simulacaoVM;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// recebe o ViewModel em memória e realiza o cálculo, devolvendo o resultado em um ViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        public CalculadoraViewModel RealizaCalculo(CalculadoraViewModel model)
        {
            SimulacaoINSS simulacao = CalculadoraViewModel.MapToSimuladorINSS(model);

            SimuladorINSS.Simular(simulacao);

            model.SimulacaoSelecionada.IdadePorExtenso = TempoPorExtenso(simulacao.Idade.Anos, simulacao.Idade.Meses, simulacao.Idade.Dias);
            model.SimulacaoSelecionada.TempoRestanteAposentadoriaPorExtenso = TempoPorExtenso(simulacao.TempoRestanteAposentadoria.Anos, simulacao.TempoRestanteAposentadoria.Meses, simulacao.TempoRestanteAposentadoria.Dias);

            //return CalculadoraViewModel.MapToCalculadoraViewModel(model, simulacao);
            model.SimulacaoINSS = simulacao;

            return model;
        }

        //public CalculadoraViewModel RealizaCalculo(CalculadoraViewModel model)
        //{
        //    try
        //    {
        //        int anos = 0, meses = 0, dias = 0;
        //        decimal anosTotal = 0;
        //        model.SimulacaoSelecionada.Anos = 0;
        //        model.SimulacaoSelecionada.Meses = 0;
        //        model.SimulacaoSelecionada.Dias = 0;
        //        foreach (var item in model.TempoContribuicoes)
        //        {
        //            CalculaTempo(item.DataAdmissao, item.DataDemissao, ref anos, ref meses, ref dias);
        //            anosTotal += CalculaTempoAnos(item.DataAdmissao, item.DataDemissao);
        //            model.SimulacaoSelecionada.Anos += anos;
        //            model.SimulacaoSelecionada.Meses += meses;
        //            model.SimulacaoSelecionada.Dias += dias;
        //        }
        //        model.SimulacaoSelecionada.TempoTotalContribuicao = TempoPorExtenso(model.SimulacaoSelecionada.Anos, model.SimulacaoSelecionada.Meses, model.SimulacaoSelecionada.Dias);

        //        model.SimulacaoSelecionada.DataCompletaTempoContribuicao = DataCompletaTempoContribuicao(DateTime.Now, model.SimulacaoSelecionada.Anos, model.SimulacaoSelecionada.Meses, model.SimulacaoSelecionada.Dias);

        //        model.SimulacaoSelecionada.PorcentagemTempoContribuicao = Math.Round((anosTotal * 100) / 35, 0);

        //        return model;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

    }
}
