using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculoPrev.DAL.Models
{
    public class Cliente : Pessoa
    {
        public string InscricaoINSS { get; set; }

        public string Nit { get; set; }
    }
}