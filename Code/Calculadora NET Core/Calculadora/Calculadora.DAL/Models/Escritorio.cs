using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculadora.DAL.Models
{
    public class Escritorio
    {
        [Key]
        public int EscritorioID { get; set; }
        public string Nome { get; set; }

        public int EnderecoID { get; set; }
        //[ForeignKey("EnderecoID")]
        public virtual Endereco Endereco { get; set; }

        public int ProprietarioID { get; set; }
        [ForeignKey("ProprietarioID")]
        public virtual Pessoa Proprietario { get; set; }

        public string Telefone { get; set; }
        public string Registro { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
