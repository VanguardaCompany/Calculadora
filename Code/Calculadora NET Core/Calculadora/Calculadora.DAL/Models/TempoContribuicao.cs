using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculadora.DAL.Models
{
    public class TempoContribuicao
    {
        [Key]
        public int TempoContribuicaoID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime DataAdmissao { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime DataDemissao { get; set; }

        /// <summary>
        /// Atividade é insalubre, perigosa ou penosa
        /// </summary>
        [Required]
        public bool AtividadeIPP { get; set; }

        public string Profissao { get; set; }

        public string Empregador { get; set; }

        //public Simulacao Simulacao { get; set; }
        public int SimulacaoID { get; set; }
        [ForeignKey("SimulacaoID")]
        public virtual Simulacao Simulacao { get; set; }
    }
}