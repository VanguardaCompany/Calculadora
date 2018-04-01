using System;
using System.Linq;
using Calculadora.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.DAL.Repository
{
    public class CalculadoraRepository : BaseRepository
    {
        private readonly VanguardaContext db;

        public CalculadoraRepository(VanguardaContext context)
        {
            db = context;
        }

        public IQueryable<Simulacao> GetSimulacoes(int idCliente)
        {
            try
            {
                IQueryable<Simulacao> simulacoes = from s in db.Simulacoes
                                                   where s.Cliente.PessoaID == idCliente
                                                   select s;

                return simulacoes;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Simulacao GetSimulacaoId(int idSimulacao)
        {
            try
            {
                return (from s in db.Simulacoes
                        where s.SimulacaoID == idSimulacao
                        select s).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void AddSimulacao(Simulacao simulacao)
        {
            try
            {
                db.Simulacoes.Add(simulacao);
                db.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public IQueryable<TempoContribuicao> GetTempoContribuicoes(int idSimulacao)
        {
            try
            {
                IQueryable<TempoContribuicao> tempoContribuicoes = from s in db.TempoContribuicoes
                                                                   where s.SimulacaoID == idSimulacao
                                                                   select s;


                return tempoContribuicoes;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public TempoContribuicao GetTempoContribuicaoId(int idTempoContribuicao)
        {
            try
            {
                return (from s in db.TempoContribuicoes
                        where s.TempoContribuicaoID == idTempoContribuicao
                        select s).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void SetTempoContribuicao(TempoContribuicao tempoContribuicao)
        {
            try
            {
                db.TempoContribuicoes.Add(tempoContribuicao);
                db.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void AddTempoContribuicao(TempoContribuicao tempoContribuicao)
        {
            try
            {
                db.TempoContribuicoes.Add(tempoContribuicao);
                db.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void UpdateTempoContribuicao(TempoContribuicao tempoContribuicao)
        {
            try
            {
                db.TempoContribuicoes.Update(tempoContribuicao);
                db.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }


    }
}
