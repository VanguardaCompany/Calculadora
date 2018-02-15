using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculoPrev.DAL.Models
{
    public class ValorIndiceCorrecao
    {
        [Key, Column(Order = 0)]

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public DateTime Data { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00000}", ApplyFormatInEditMode = true)]
        public double Valor { get; set; }

        [Key, Column(Order = 1)]
        public int IndiceCorrecaoID { get; set; }

        [ForeignKey("IndiceCorrecaoID")]
        public virtual IndiceCorrecao IndiceCorrecao { get; set; }
    }
}