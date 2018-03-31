using System;

namespace Calculadora.Simulador.Models
{
    public class Duracao
    {
        private DateTime dataInicio;
        private DateTime dataFim;
        private int dias;
        private int meses;
        private int anos;

        public Duracao(DateTime dataInicio, DateTime dataFim)
        {
            this.dataInicio = dataInicio;
            this.dataFim = dataFim;

            DuracaoEntreDatas(this.dataInicio, this.dataFim, out this.anos, out this.meses, out this.dias);
        }

        public Duracao(int totalDias)
        {
            this.dataInicio = DateTime.Now.AddDays(-totalDias);
            this.dataFim = DateTime.Now;

            DuracaoEntreDatas(this.dataInicio, this.dataFim, out this.anos, out this.meses, out this.dias);
        }

        public int Dias
        {
            get
            {
                return this.dias;
                //if (this.dataInicio != null)
                //    this.totalDias = (int)((TimeSpan)(this.dataFim - this.dataInicio)).TotalDays;
                //return Convert.ToInt32(Math.Truncate((this.totalDias % 365.0) % 30));
            }
        }

        public int Meses
        {
            get
            {
                return this.meses;
                //if (this.dataInicio != null)
                //    this.totalDias = (int)((TimeSpan)(this.dataFim - this.dataInicio)).TotalDays;
                //return Convert.ToInt32(Math.Truncate(this.totalDias % 365.0) / 30);
            }
        }

        public int Anos
        {
            get
            {
                return this.anos;
                //if (this.dataInicio != null)
                //    this.totalDias = (int)((TimeSpan)(this.dataFim - this.dataInicio)).TotalDays;
                //return (this.totalDias / 365);
            }
        }

        public TimeSpan Periodo
        {
            get
            {
                if (this.dataInicio != null) return ((TimeSpan)(this.dataFim - this.dataInicio));
                else
                    return new TimeSpan(this.TotalDias, 0, 0, 0);
            }
        }

        public int TotalDias
        {
            get
            {
                return ((int)(this.dataFim - this.dataInicio).TotalDays);
            }
        }

        private void DuracaoEntreDatas(DateTime d1, DateTime d2, out int years, out int months, out int days)
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
                days = DateTime.DaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
            }
            else
            {
                days = d1.Day - d2.Day;
            }
            // compute years & actual months
            years = months / 12;
            months -= years * 12;
        }

    }
}
