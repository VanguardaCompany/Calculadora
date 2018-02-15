using System;
using System.ComponentModel.DataAnnotations;

namespace CalculoPrev.DAL.Models
{
    public class Limite
    {
        [Key]
        public int LimiteID { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public DateTime Data { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double TetoContribuicao { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double TetoRMI { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double MenorValorTeto { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public double MaiorValorTeto { get; set; }

    }
}