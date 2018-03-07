using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calculadora.DAL.Models
{
    public class Simulacao
    {
        [Key]
        public int SimulacaoID { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime Data { get; set; }

        public Cliente Cliente { get; set; }

        public virtual ICollection<TempoContribuicao> TempoContribuicoes{ get; set; }
    }
}
