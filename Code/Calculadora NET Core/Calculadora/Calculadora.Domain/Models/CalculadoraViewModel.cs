using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Calculadora.DAL.Models;
using Calculadora.Simulador.Models;
using PagedList.Core;

namespace Calculadora.Domain.Models
{
    public class CalculadoraViewModel
    {
        public ICollection<Cliente> Clientes { get; set; }
        public Cliente ClienteSelecionado { get; set; }
        public int ClienteSelecionadoID { get; set; }
        public ICollection<SimulacaoViewModel> Simulacoes { get; set; }
        public SimulacaoViewModel SimulacaoSelecionada { get; set; }
        public ICollection<TempoContribuicaoViewModel> TempoContribuicoes { get; set; }

        public static SimulacaoINSS MapToSimuladorINSS(CalculadoraViewModel model)
        {
            SimulacaoINSS simulacao = new SimulacaoINSS(model.ClienteSelecionado.Nascimento, model.ClienteSelecionado.Sexo.ToString());
            foreach (TempoContribuicaoViewModel tempoContribuicao in model.TempoContribuicoes)
            {
                simulacao.ListaVinculosTrabalhistas.Add(new VinculoTrabalhista(tempoContribuicao.DataAdmissao, tempoContribuicao.DataDemissao, tempoContribuicao.valorContribuicao, tempoContribuicao.Empregador));
            }

            return simulacao;
        }

        public static CalculadoraViewModel MapToCalculadoraViewModel(CalculadoraViewModel model, SimulacaoINSS simulacao)
        {

            foreach (VinculoTrabalhista vinculo in simulacao.ListaVinculosTrabalhistas)
            {
               // model.SimulacaoSelecionada.TempoContribuicoes
            }

            return model;
        }
    }
    public class SimulacaoViewModel
    {
        [Key]
        public int SimulacaoID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        public Cliente Cliente { get; set; }

        /// <summary>
        /// Data em que completará 35 anos de contribuição
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public DateTime DataCompletaTempoContribuicao { get; set; }

        /// <summary>
        /// Tempo total de contribuição por extenso
        /// </summary>
        public string TempoTotalContribuicao { get; set; }

        /// <summary>
        /// Porcentagem de Tempo de contribuição, sendo que 35 anos é 100%
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal PorcentagemTempoContribuicao { get; set; }

        /// <summary>
        /// Data em que se aposentará por tempo de contribuição
        /// </summary>
        public DateTime DataAposentadoriaTempoContribuicao { get; set; }

        /// <summary>
        /// Data em que se aposentará por Idade
        /// </summary>
        public DateTime DataAposentadoriaIdade { get; set; }


        public int Anos { get; set; }
        public int Meses { get; set; }
        public int Dias { get; set; }

        public virtual ICollection<TempoContribuicaoViewModel> TempoContribuicoes { get; set; }

        public Simulacao SimulacaoToModel(SimulacaoViewModel model)
        {
            Simulacao s = new Simulacao();

            s.Data = model.Data;
            s.SimulacaoID = model.SimulacaoID;
            s.ClienteID = model.Cliente.PessoaID;
            return s;
        }

        public SimulacaoViewModel SimulacaoToViewModel(Simulacao model)
        {
            SimulacaoViewModel s = new SimulacaoViewModel();

            s.Data = model.Data;
            s.SimulacaoID = model.SimulacaoID;
            s.Cliente.PessoaID = model.ClienteID;
            return s;
        }
    }
    public class TempoContribuicaoViewModel
    {
        [Key]
        public int TempoContribuicaoID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataAdmissao { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataDemissao { get; set; }

        /// <summary>
        /// Atividade é insalubre, perigosa ou penosa
        /// </summary>
        [Required]
        public bool AtividadeIPP { get; set; }

        public string Profissao { get; set; }

        public string Empregador { get; set; }

        public double valorContribuicao { get; set; }

        //public Simulacao Simulacao { get; set; }
        public int SimulacaoID { get; set; }

        public TempoContribuicao TempoContribuicaoToModel(TempoContribuicaoViewModel model)
        {
            TempoContribuicao tc = new TempoContribuicao();

            tc.AtividadeIPP = model.AtividadeIPP;
            tc.DataAdmissao = model.DataAdmissao;
            tc.DataDemissao = model.DataDemissao;
            tc.Profissao = model.Profissao;
            tc.Empregador = model.Empregador;
            tc.SimulacaoID = model.SimulacaoID;
            tc.TempoContribuicaoID = model.TempoContribuicaoID;
            return tc;
        }
    }
}
