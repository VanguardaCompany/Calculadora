using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Calculadora.DAL.Models
{
    public class CorrecaoDiferencas
    {
        [Key]
        public int CorrecaoDiferencasID { get; set; }
        public DateTime DataInicio { get; set; }
        public int Indice { get; set; }
        public int PrimeiroMes { get; set; }
        public int UltimoMes { get; set; }
        public int Porcentagem { get; set; }
    }
}
