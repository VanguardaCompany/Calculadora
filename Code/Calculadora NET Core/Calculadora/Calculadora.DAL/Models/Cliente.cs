using System;
using System.Collections.Generic;
using System.Text;

namespace Calculadora.DAL.Models
{
    public class Cliente : Pessoa
    {
        public string InscricaoINSS { get; set; }

        public string Nit { get; set; }
    }
    
}
