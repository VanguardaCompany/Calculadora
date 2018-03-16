using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Calculadora.DAL.Models
{
    public class Cliente : Pessoa
    {
        public string InscricaoINSS { get; set; }

        public string Nit { get; set; }

        public int EscritorioID { get; set; }
        //[ForeignKey("EscritorioID")]
        public virtual Escritorio Escritorio { get; set; }
    }
    
}
