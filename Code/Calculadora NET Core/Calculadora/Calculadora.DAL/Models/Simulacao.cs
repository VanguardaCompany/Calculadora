using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculadora.DAL.Models
{
    public class Simulacao
    {
        [Key]
        public int SimulacaoID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime Data { get; set; }


        public string Nome { get; set; }

        public int ClienteID { get; set; }
        [ForeignKey("ClienteID")]
        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<TempoContribuicao> TempoContribuicoes { get; set; }
    }
}
