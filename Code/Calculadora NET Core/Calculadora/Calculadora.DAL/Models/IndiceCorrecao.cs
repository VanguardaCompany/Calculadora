using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculadora.DAL.Models
{
    public enum EnumPeriodicidadeIC
    {
        [Display(Name = "Diário")]
        Diario = 1,
        [Display(Name = "Mensal")]
        Mensal = 2
    }

    public enum EnumTipoIC
    {
        [Display(Name = "Índice em percentagem")]
        Percentagem = 1,
        [Display(Name = "Índice em valor")]
        Valor = 2,
        [Display(Name = "Índice com data de aniversário")]
        DataAniversario = 3,
        [Display(Name = "Índice para somatório (sem capitalização)")]
        Somatorio = 4
    }

    public class IndiceCorrecao : BaseModel
    {
        [Key]
        public int IndiceCorrecaoID { get; set; }

        [MaxLength(150)]
        public string Nome { get; set; }

        public EnumPeriodicidadeIC Periodicidade { get; set; }

        public EnumTipoIC Tipo { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public DateTime DataInicio { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public DateTime DataFim { get; set; }

        public virtual ICollection<ValorIndiceCorrecao> Valores { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [MaxLength(500)]
        public string Informacoes { get; set; }

        public bool Extinto { get; set; }

        public bool Atualizado { get; set; }

        [NotMapped]
        public double UltimoValor { get; set; }

    }
}
