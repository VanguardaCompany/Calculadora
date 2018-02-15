using System;
using System.ComponentModel.DataAnnotations;

namespace CalculoPrev.DAL.Models
{
    public class ReajusteRMI
    {
        [Key]
        public int ReajusteRMIID { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public DateTime Data { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double ReajusteTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator1 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator3 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator4 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator5 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator6 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator7 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator8 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator9 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator10 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator11 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public double Fator12 { get; set; }

        public string linkPrevidencia { get; set; }
    }
}