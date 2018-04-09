using System;
using System.IO;
using System.Linq;

namespace Calculadora.Simulador.Models
{
    public class Duracao
    {
        private int dias;
        private int meses;
        private int anos;

        public Duracao(DateTime dataInicio, DateTime dataFim)
        {
            DuracaoEntreDatas(dataInicio, dataFim.AddSeconds(1), out this.anos, out this.meses, out this.dias);
        }

        public Duracao(int anos, int meses, int dias)
        {
            this.anos = anos;
            this.meses = meses;
            this.dias = dias;
        }

        public int Dias
        {
            get
            {
                return this.dias;
            }
        }

        public int Meses
        {
            get
            {
                return this.meses;
            }
        }

        public int Anos
        {
            get
            {
                return this.anos;
            }
        }

        private int DuracaoEntreDatas(DateTime d1, DateTime d2, out int years, out int months, out int days)
        {
            // compute & return the difference of two dates,
            // returning years, months & days
            // d1 should be the larger (newest) of the two dates
            // we want d1 to be the larger (newest) date
            // flip if we need to
            if (d1 < d2)
            {
                DateTime d3 = d2;
                d2 = d1;
                d1 = d3;
            }

            // compute difference in total months
            months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);

            // based upon the 'days',
            // adjust months & compute actual days difference
            if (d1.Day < d2.Day)
            {
                months--;
                days = 30 - d2.Day + d1.Day;
            }
            else
            {
                days = d1.Day - d2.Day;
            }
            // compute years & actual months
            years = months / 12;
            months -= years * 12;

            return ((years * 12 * 30) + (months * 30) + days);
        }

        public static Duracao SomarDuracoes(Duracao duracao1, Duracao duracao2)
        {

            if (duracao1 == null) duracao1 = new Duracao(0, 0, 0);
            if (duracao2 == null) duracao2 = new Duracao(0, 0, 0);

            int anos, meses, dias = 0;

            dias = duracao1.dias + duracao2.dias;
            meses = duracao1.meses + duracao2.meses;
            anos = duracao1.anos + duracao2.anos;

            if (dias >= 30)
            {
                meses += dias / 30;
                dias = dias % 30;
            }

            if (meses >= 12)
            {
                anos += meses / 12;
                meses = meses % 12;
            }

            return new Duracao(anos, meses, dias);
        }

        public static Duracao SubtrairDuracoes(Duracao duracao1, Duracao duracao2)
        {
            if (duracao1 == null) duracao1 = new Duracao(0, 0, 0);
            if (duracao2 == null) duracao2 = new Duracao(0, 0, 0);

            int anos, meses, dias = 0;

            dias = duracao1.dias - duracao2.dias;
            meses = duracao1.meses - duracao2.meses;
            anos = duracao1.anos - duracao2.anos;

            if (dias < 0)
            {
                meses--;
                dias = 30 + dias;
            }

            if (meses < 0)
            {
                anos--;
                meses = 12 + meses;
            }

            return new Duracao(anos, meses, dias);
        }
    }
}
