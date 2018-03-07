using System;
using Xunit;
using Calculadora.Domain.Models;
using Calculadora.Domain.Business;
using Calculadora.DAL;
using FluentAssertions;

namespace Calculadora.Test
{
    public static class TesteCalculoTempoContribuicao
    {
        [Theory]
        [InlineData(2,3,1)]
        public static void TesteCalculaTempo(int panos, int pmeses, int pdias)
        {
            DateTime Inicio = DateTime.Now;
            Inicio = Inicio.AddDays(-pdias);
            Inicio = Inicio.AddMonths(-pmeses);
            Inicio = Inicio.AddYears(-panos);
            
            DateTime Fim = DateTime.Now;
            int anos = 0, meses = 0, dias = 0;
            CalculadoraBusiness.CalculaTempo(Inicio, Fim,ref anos,ref meses,ref dias);
            Console.WriteLine(anos+"-"+meses+"-"+dias);
            anos.Should().Be(panos, "Anos errado");
            meses.Should().Be(pmeses, "Meses errado");
            dias.Should().Be(pdias, "Dias errado");
        }
        [Theory]
        [InlineData(25, 2, 24, "2018-01-17","2027-10-24")]
        public static void TesteDataCompletaTempoContribuicao(int panos, int pmeses, int pdias, string pdata,string pesperado)
        {
            DateTime dataEsperada = DateTime.ParseExact(pesperado, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dataConsulta = DateTime.ParseExact(pdata, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            DateTime resultado = CalculadoraBusiness.DataCompletaTempoContribuicao(dataConsulta, panos, pmeses, pdias);
           
            DateTime esperado = DateTime.ParseExact(pesperado, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            
            esperado.Should().Be(resultado, "data errada");
        }
    }
}
