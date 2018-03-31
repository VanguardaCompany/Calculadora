using System;

namespace Calculadora.Simulador.Models
{
    public class VinculoTrabalhista
    {
        private string id;
        private DateTime dataInicio;
        private DateTime dataFim;
        private DateTime dataInicioSemIntersecao;
        private DateTime dataFimSemIntersecao;
        private double valorContribuicao;
        private string nomeEmpregador;
        private bool priorizadoSistema;
        private bool priorizadoUsuario;
        private bool inputUsuario;

        public VinculoTrabalhista(DateTime dataInicio, DateTime dataFim, double valorContribuicao, string nomeEmpregador,
                                  bool priorizadoSistema = false, bool inputUsuario = true, bool periodoSemLancamento = false, string idPai = null)
        {
            this.dataInicio = dataInicio;
            this.dataFim = dataFim;
            this.dataInicioSemIntersecao = dataInicio;
            this.dataFimSemIntersecao = dataFim;
            this.valorContribuicao = valorContribuicao;
            this.nomeEmpregador = nomeEmpregador;
            this.inputUsuario = inputUsuario;
            this.priorizadoSistema = priorizadoSistema;
            this.periodoSemLancamento = periodoSemLancamento;
            this.id = (string.IsNullOrEmpty(idPai)) ? Guid.NewGuid().ToString() : idPai;
            this.PeriodoEmDuplicidade = new Duracao(0);
        }

        public DateTime DataFim
        {
            get { return this.dataFim; }
            set { this.dataFim = value; }
        }

        public DateTime DataInicio
        {
            get { return this.dataInicio; }
            set { this.dataInicio = value; }
        }

        internal DateTime DataFimSemIntersecao
        {
            get { return this.dataFimSemIntersecao; }
            set { this.dataFimSemIntersecao = value; }
        }

        internal DateTime DataInicioSemIntersecao
        {
            get { return this.dataInicioSemIntersecao; }
            set { this.dataInicioSemIntersecao = value; }
        }

        public double ValorContribuicao
        {
            get { return this.valorContribuicao; }
            set { this.valorContribuicao = value; }
        }

        public string NomeEmpregador
        {
            get { return this.nomeEmpregador; }
            set { this.nomeEmpregador = value; }
        }

        public Duracao TempoTotal
        {
            get { return new Duracao(dataInicio, dataFim); }
        }

        public Duracao TempoLiquido
        {
            get { return new Duracao(this.TempoTotal.TotalDias - this.PeriodoEmDuplicidade.TotalDias); }
        }

        public Duracao PeriodoEmDuplicidade
        {
            get; set; //{ return new Duracao((int)((dataInicio - dataInicioSemIntersecao).TotalDays + (dataFim - dataFimSemIntersecao).TotalDays)); }
        }

        internal bool PriorizadoSistema
        {
            get { return priorizadoSistema; }
            set { priorizadoSistema = value; }
        }

        internal bool PriorizadoUsuario
        {
            get { return priorizadoUsuario; }
            set { priorizadoUsuario = value; }
        }

        internal bool Priorizado
        {
            get { return priorizadoUsuario || priorizadoSistema; }
        }

        public bool PeriodoSemLancamento
        {
            get { return periodoSemLancamento; }
            set { periodoSemLancamento = value; }
        }

        internal bool InputUsuario
        {
            get { return inputUsuario; }
            set { inputUsuario = value; }
        }

        public string Id { get => this.id; }

        private bool periodoSemLancamento;
    }
}
