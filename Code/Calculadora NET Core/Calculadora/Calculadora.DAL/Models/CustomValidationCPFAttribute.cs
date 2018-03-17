using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Calculadora.DAL.Models
{
    [NotMapped]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CustomValidationCPFAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public CustomValidationCPFAttribute()
        {

        }

        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult("CPF inválido");
            bool valido = ValidaCPF(value.ToString());
            if (valido)return ValidationResult.Success;
            else return new ValidationResult("CPF inválido");

        }

        /// <summary>
        /// Validação client
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>

        //public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{

        //    yield return new ModelClientValidationRule
        //    {
        //        ErrorMessage = this.FormatErrorMessage(null),
        //        ValidationType = "customvalidationcpf"
        //    };
        //}

        // <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>

        public static string RemoveNaoNumericos(string text)
        {

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;

        }

        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>

        public static bool ValidaCPF(string cpf)
        {

            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = RemoveNaoNumericos(cpf);
            if (cpf.Length > 11)
                return false;
            if (cpf.Length < 11)
                return false;
            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {

                if (numeros[9] != 0)
                    return false;

            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {

                if (numeros[10] != 0)
                    return false;

            }

            else
            if (numeros[10] != 11 - resultado)
                return false;

            return true;

        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
        }
    }

}
