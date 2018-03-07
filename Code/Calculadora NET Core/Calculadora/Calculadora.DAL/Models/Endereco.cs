using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Calculadora.DAL.Models
{
    public enum EnumLocal
    {
        [Display(Name = "Residência")]
        Residencia = 1,
        [Display(Name = "Matriz")]
        Matriz = 2,
        [Display(Name = "Escritório")]
        Escritorio = 3,
        [Display(Name = "Secretaria")]
        Secretaria = 4
    }

    public enum EnumLogradouro
    {
        [Display(Name = "Rua")]
        Rua = 1,
        [Display(Name = "Avenida")]
        Avenida = 2,
        [Display(Name = "Praça")]
        Praca = 3
    }

    public class Endereco
    {
        [Key]
        public int EnderecoID { get; set; }

        public EnumLocal Local { get; set; }

        public EnumLogradouro Logradouro { get; set; }

        public string NomeLogradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Pais { get; set; }

        public string Cep { get; set; }

        public string Contato { get; set; }

        public string Telefone { get; set; }



    }
}
