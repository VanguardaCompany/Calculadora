using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Calculadora.DAL.Models
{
    public class ParametroCalculoPrevidenciario
    {
        [Key]
        public int ParametroCalculoPrevidenciarioID { get; set; }
        public int Ano { get; set; }
        public int ValorMulher { get; set; }
        public int ValorHomem { get; set; }
        public string TipoParametro { get; set; }
    }
}
