using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Calculadora.DAL.Models
{
    public class ExpectativaVida
    {
        [Key]
        public int ExpectativaVidaID { get; set; }
        public DateTime Data { get; set; }
        public int Valor { get; set; }
    }
}
