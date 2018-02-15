using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalculoPrev.DAL.Models
{
    public class ExpectativaVida
    {
        [Key]
        public int ExpectativaVidaID { get; set; }
        public DateTime Data { get; set; }
        public int Valor { get; set; }
    }
}