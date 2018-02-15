using System;
using System.ComponentModel.DataAnnotations;

namespace CalculoPrev.DAL.Models
{
    public class BeneficioRecebido
    {
        [Key]
        public int BeneficioRecebidoID { get; set; }
        public DateTime Data { get; set; }
        public int Valor { get; set; }
    }
}