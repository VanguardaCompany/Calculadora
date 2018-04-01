using System;
using System.Collections.Generic;

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
        private EnumSexo sexo;

        public SimulacaoINSS(DateTime dataNascimento, string sexo)
        {
            this.dataNascimento = dataNascimento;
            this.listaVinculosTrabalhistas = new List<VinculoTrabalhista>();
            this.listaVinculosTrabalhistasAuxiliar = new List<VinculoTrabalhista>();
            this.sexo = sexo == "2" ? EnumSexo.Feminino : EnumSexo.Masculino;
        }

        public Duracao TempoSimulado
        {
            get { return this.tempoSimulado; }
            set { this.tempoSimulado = value; }
        }

        public Duracao Idade
        {
            get { return new Duracao(dataNascimento, DateTime.Now); }
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
            get { return new Duracao((sexo == EnumSexo.Masculino) ? ((35 * 365) - TempoSimulado.TotalDias) : ((30 * 365) - TempoSimulado.TotalDias)); }
        }

        public int PontuacaoRestanteAposentadoria
        {
            get { return ((sexo == EnumSexo.Masculino) ? 95 : 85) - this.PontuacaoAtual; }
        }

        public int PontuacaoAtual
        {
            get { return this.tempoSimulado.Anos + this.Idade.Anos; }
        }

    }


}
