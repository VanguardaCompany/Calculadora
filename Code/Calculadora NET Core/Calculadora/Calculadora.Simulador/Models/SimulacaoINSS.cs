using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculadora.Simulador.Models
{
    public enum EnumSexo
    {
        Masculino = 1,
        Feminino = 2
    }

    public class SimulacaoINSS
    {
        private Duracao tempoSimulado;
        private DateTime dataNascimento;
        private List<VinculoTrabalhista> listaVinculosTrabalhistasAuxiliar;
        private List<VinculoTrabalhista> listaVinculosTrabalhistas;
        private List<ParametroCalculoPrevidenciario> listaFatoresPrevidenciarios;
        private int tempoMinimoAposentadoria;
        private EnumSexo sexo;

        public SimulacaoINSS(DateTime dataNascimento, string sexo)
        {
            this.dataNascimento = dataNascimento;
            this.listaVinculosTrabalhistas = new List<VinculoTrabalhista>();
            this.listaVinculosTrabalhistasAuxiliar = new List<VinculoTrabalhista>();
            this.sexo = (sexo == "2" ? EnumSexo.Feminino : EnumSexo.Masculino);
            this.tempoMinimoAposentadoria = (this.sexo == EnumSexo.Masculino ? 35 : 30);
            this.listaFatoresPrevidenciarios = new List<ParametroCalculoPrevidenciario>();

        }

        public Duracao TempoSimulado
        {
            get { return this.tempoSimulado; }
            set { this.tempoSimulado = value; }
        }

        public Duracao Idade
        {
            get { return new Duracao(dataNascimento, DateTime.Now.Date); }
        }

        public DateTime DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }

        internal List<VinculoTrabalhista> ListaVinculosTrabalhistasAuxiliar
        {
            get { return listaVinculosTrabalhistasAuxiliar; }
            set { listaVinculosTrabalhistasAuxiliar = value; }
        }

        public List<VinculoTrabalhista> ListaVinculosTrabalhistas
        {
            get { return listaVinculosTrabalhistas; }
            set { listaVinculosTrabalhistas = value; }
        }

        public Duracao TempoRestanteAposentadoria
        {
            get { return Duracao.SomarDuracoes(new Duracao(this.tempoMinimoAposentadoria, 0, 0), TempoSimulado); }
        }

        public int PontuacaoRestanteAposentadoria
        {
            get { return this.FatorPrevidenciarioAtual - this.PontuacaoAtual; }
        }

        public int PontuacaoAtual
        {
            get { return this.tempoSimulado.Anos + this.Idade.Anos; }
        }

        public List<ParametroCalculoPrevidenciario> ListaFatoresPrevidenciarios { get { return this.listaFatoresPrevidenciarios; } set { this.listaFatoresPrevidenciarios = value; } }

        private int FatorPrevidenciarioAtual
        {
            get
            {
                ParametroCalculoPrevidenciario fator = listaFatoresPrevidenciarios.Where(f => f.Ano == DateTime.Now.Year).FirstOrDefault();
                return (this.sexo == EnumSexo.Masculino) ? ((fator != null) ? fator.ValorHomem : 95) : ((fator != null) ? fator.ValorMulher : 85);
            }
        }

        public ParametroCalculoPrevidenciario TempoMinimoAposentadoria { set { this.tempoMinimoAposentadoria = (this.sexo == EnumSexo.Masculino) ? value.ValorHomem : value.ValorMulher; } }
    }


}
