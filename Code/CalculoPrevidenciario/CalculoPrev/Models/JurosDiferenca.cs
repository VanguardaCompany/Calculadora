using System;
using System.ComponentModel.DataAnnotations;

namespace CalculoPrev.DAL.Models
{
    
    public class JurosDiferenca
    {
        [Key]
        public int JurosDiferencaID { get; set; }
        public DateTime DataInicio { get; set; }
        public int Percentual { get; set; }
        public int TipoJuros { get; set; }
        public int Capitalizacao { get; set; }
    }
}