using Calculadora.DAL.Models;
using Calculadora.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora.Domain.Business
{
    public class BaseBusiness
    {
        public static decimal CalculaTempoAnos(DateTime dataInicio, DateTime dataFim)
        {
            double years = (dataFim - dataInicio).TotalDays/365.24;
            return (decimal)years;
        }
        /// <summary>
        /// Calcula o tempo entre duas datas devolvendo em parâmetros por referência em anos, meses, dias
        /// </summary>
        /// <param name="dataInicio"></param>
        /// <param name="dataFim"></param>
        /// <param name="anos"></param>
        /// <param name="meses"></param>
        /// <param name="dias"></param>
        public static void CalculaTempo(DateTime dataInicio, DateTime dataFim, ref int anos, ref int meses, ref int dias)
        {
            if ((dataFim.Year - dataInicio.Year) > 0 ||
                (((dataFim.Year - dataInicio.Year) == 0) && ((dataInicio.Month < dataFim.Month) ||
                  ((dataInicio.Month == dataFim.Month) && (dataInicio.Day <= dataFim.Day)))))
            {
                int DaysIndataInicioMonth = DateTime.DaysInMonth(dataInicio.Year, dataInicio.Month);
                int DaysRemain = dataFim.Day + (DaysIndataInicioMonth - dataInicio.Day);

                if (dataFim.Month > dataInicio.Month)
                {
                    anos = dataFim.Year - dataInicio.Year;
                    meses = dataFim.Month - (dataInicio.Month + 1) + Math.Abs(DaysRemain / DaysIndataInicioMonth);
                    dias = (DaysRemain % DaysIndataInicioMonth + DaysIndataInicioMonth) % DaysIndataInicioMonth;
                }
                else if (dataFim.Month == dataInicio.Month)
                {
                    if (dataFim.Day >= dataInicio.Day)
                    {
                        anos = dataFim.Year - dataInicio.Year;
                        meses = 0;
                        dias = dataFim.Day - dataInicio.Day;
                    }
                    else
                    {
                        anos = (dataFim.Year - 1) - dataInicio.Year;
                        meses = 11;
                        dias = DateTime.DaysInMonth(dataInicio.Year, dataInicio.Month) - (dataInicio.Day - dataFim.Day);
                    }
                }
                else
                {
                    anos = (dataFim.Year - 1) - dataInicio.Year;
                    meses = dataFim.Month + (11 - dataInicio.Month) + Math.Abs(DaysRemain / DaysIndataInicioMonth);
                    dias = (DaysRemain % DaysIndataInicioMonth + DaysIndataInicioMonth) % DaysIndataInicioMonth;
                }
            }
            else
            {
                anos = 0;
                meses = 0;
                dias = 0;
            }
        }

        public string TempoPorExtenso(int anos, int meses, int dias)
        {
            string tempo = "";
            if (anos > 0)
                if (anos == 1)
                    tempo = tempo + anos + " ano ";
                else
                    tempo = tempo + anos + " anos ";
            if (meses > 0)
            {
                if (!string.IsNullOrEmpty(tempo))
                    tempo = dias > 0 ? tempo + ", " : tempo + " e ";
                if (meses == 1)
                    tempo = tempo + meses + " mês ";
                else
                    tempo = tempo + meses + " meses ";
            }
            if (dias > 0)
            {
                if (!string.IsNullOrEmpty(tempo))
                    tempo = tempo + "e ";
                if (dias == 1)
                    tempo = tempo + dias + " dia";
                else
                    tempo = tempo + dias + " dias";
            }

            return tempo;
        }

        #region ToViewModel
        public SimulacaoViewModel SimulacaoToViewModel(Simulacao simulacao)
        {

            SimulacaoViewModel simulacaoVM = new SimulacaoViewModel();
            simulacaoVM.Cliente = simulacao.Cliente;
            simulacaoVM.Data = simulacao.Data;
            if (simulacao.TempoContribuicoes != null)
                simulacaoVM.TempoContribuicoes = TempoContribuicoesToViewModel(simulacao.TempoContribuicoes.AsQueryable());
            simulacaoVM.SimulacaoID = simulacao.SimulacaoID;

            return simulacaoVM;
        }
        public ICollection<SimulacaoViewModel> SimulacoesToViewModel(IQueryable<Simulacao> simulacoes)
        {
            List<SimulacaoViewModel> simulacoesVM = new List<SimulacaoViewModel>();

            foreach (var item in simulacoes)
            {
                SimulacaoViewModel simulacaoVM = new SimulacaoViewModel();
                simulacaoVM.Cliente = item.Cliente;
                simulacaoVM.Data = item.Data;
                if (item.TempoContribuicoes != null)
                    simulacaoVM.TempoContribuicoes = TempoContribuicoesToViewModel(item.TempoContribuicoes.AsQueryable());
                simulacaoVM.SimulacaoID = item.SimulacaoID;
                //simulacoesVM = simulacoesVM.Concat(new SimulacaoViewModel[] { simulacaoVM });
                simulacoesVM.Add(simulacaoVM);
            }
            return simulacoesVM;
        }
        public Simulacao ViewModelToSimulacao(SimulacaoViewModel simulacaoVM)
        {
            Simulacao simulacao = new Simulacao();
            simulacao.Cliente = simulacaoVM.Cliente;
            simulacao.Data = simulacaoVM.Data;

            return simulacao;
        }
        public TempoContribuicaoViewModel TempoContribuicaoToViewModel(TempoContribuicao tempoContribuicao)
        {
            TempoContribuicaoViewModel TempoContribuicaoVM = new TempoContribuicaoViewModel();
            TempoContribuicaoVM.AtividadeIPP = tempoContribuicao.AtividadeIPP;
            TempoContribuicaoVM.DataAdmissao = tempoContribuicao.DataAdmissao;
            TempoContribuicaoVM.DataDemissao = tempoContribuicao.DataDemissao;
            TempoContribuicaoVM.Empregador = tempoContribuicao.Empregador;
            TempoContribuicaoVM.TempoContribuicaoID = tempoContribuicao.TempoContribuicaoID;
            TempoContribuicaoVM.Profissao = tempoContribuicao.Profissao;

            return TempoContribuicaoVM;
        }
        public ICollection<TempoContribuicaoViewModel> TempoContribuicoesToViewModel(IQueryable<TempoContribuicao> tempoContribuicoes)
        {
            List<TempoContribuicaoViewModel> tempoContribuicoesVM = new List<TempoContribuicaoViewModel>();

            foreach (var item in tempoContribuicoes)
            {
                TempoContribuicaoViewModel tempoContribuicaoVM = new TempoContribuicaoViewModel();
                tempoContribuicaoVM.AtividadeIPP = item.AtividadeIPP;
                tempoContribuicaoVM.DataAdmissao = item.DataAdmissao;
                tempoContribuicaoVM.DataDemissao = item.DataDemissao;
                tempoContribuicaoVM.Empregador = item.Empregador;
                tempoContribuicaoVM.TempoContribuicaoID = item.TempoContribuicaoID;
                tempoContribuicaoVM.Profissao = item.Profissao;
                tempoContribuicaoVM.SimulacaoID = item.SimulacaoID;
                tempoContribuicoesVM.Add(tempoContribuicaoVM);
            }
            return tempoContribuicoesVM;
        }
        public TempoContribuicao ViewModelToTempoContribuicao(TempoContribuicaoViewModel tempoContribuicaoVM)
        {
            TempoContribuicao tempoContribuicao = new TempoContribuicao();
            tempoContribuicao.AtividadeIPP = tempoContribuicaoVM.AtividadeIPP;
            tempoContribuicao.DataAdmissao = tempoContribuicaoVM.DataAdmissao;
            tempoContribuicao.DataDemissao = tempoContribuicaoVM.DataDemissao;
            tempoContribuicao.Empregador = tempoContribuicaoVM.Empregador;
            tempoContribuicao.Profissao = tempoContribuicaoVM.Profissao;
            tempoContribuicao.SimulacaoID = tempoContribuicaoVM.SimulacaoID;

            return tempoContribuicao;
        }

        #endregion
    }
}
