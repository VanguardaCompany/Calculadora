using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalculoPrev.DAL.Models
{
    public class Escritorio
    {
        [Key]
        public int EscritorioID { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        
        //public int PessoaID { get; set; }

        //[ForeignKey("PessoaID")]
        //public virtual Pessoa Proprietario { get; set; }

        public string Telefone { get; set; }
        public string Registro { get; set; }

        public virtual ICollection<Pessoa> Pessoas { get; set; }
    }
}