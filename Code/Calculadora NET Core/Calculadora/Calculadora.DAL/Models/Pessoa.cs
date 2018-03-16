using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculadora.DAL.Models
{
    public enum EnumSexo
    {
        [Display(Name = "Masculino")]
        Masculino = 1,
        [Display(Name = "Feminino")]
        Feminino = 2
    }
    public enum EnumTratamento
    {
        [Display(Name = "Sr.")]
        Sr = 1,
        [Display(Name = "Sra.")]
        Sra = 2,
        [Display(Name = "Srta.")]
        Srta = 3,
        [Display(Name = "Menor")]
        Menor = 4
    }
    public enum EnumEstadoCivil
    {
        [Display(Name = "Solteiro(a)")]
        Solteiro = 1,
        [Display(Name = "Casado(a)")]
        Casado = 2,
        [Display(Name = "Divorciado(a)")]
        Divorciado = 3,
        [Display(Name = "Viúvo(a)")]
        Viuvo = 4,
        [Display(Name = "Separado(a)")]
        Separado = 5,
        [Display(Name = "União estável")]
        UniaoEstavel = 6
    }
    public enum EnumDocumentoIdentificacao
    {
        [Display(Name = "RG")]
        RG = 1,
        [Display(Name = "CTPS")]
        CTPS = 2,
        [Display(Name = "Passaporte")]
        Passaporte = 3
    }
    public enum EnumTipoTelefone
    {
        [Display(Name = "Celular")]
        Celular = 1,
        [Display(Name = "Fixo")]
        Fixo = 2,
        [Display(Name = "Fax")]
        Fax = 3
    }


    public class Pessoa
    {
        [Key]
        public int PessoaID { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        public EnumSexo Sexo { get; set; }

        public EnumTratamento Tratamento { get; set; }

        public EnumEstadoCivil EstadoCivil { get; set; }

        public string Profissao { get; set; }

        public EnumDocumentoIdentificacao DocumentoIdentificacao { get; set; }

        public string NumeroDocumentoIdentificacao { get; set; }

        public string Email { get; set; }

        public string HomePage { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public EnumTipoTelefone TipoTelefone1 { get; set; }

        public EnumTipoTelefone TipoTelefone2 { get; set; }

        public EnumTipoTelefone TipoTelefone3 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone1 { get; set; }

        public string Telefone2 { get; set; }

        public string Telefone3 { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cpf { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Nascimento { get; set; }

        public int EnderecoID { get; set; }
        //[ForeignKey("EnderecoID")]
        public virtual Endereco Endereco { get; set; }

    }
}
