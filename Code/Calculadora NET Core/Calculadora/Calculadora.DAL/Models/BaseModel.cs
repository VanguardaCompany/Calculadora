using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculadora.DAL.Models
{

    public class BaseModel
    {
        [NotMapped]
        public bool Readonly { get; set; }
    }
}
