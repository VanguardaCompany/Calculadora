using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Calculadora.DAL.Models;

namespace Calculadora.DAL
{
    public class VanguardaContext : DbContext
    {
        public DbSet<Escritorio> Escritorios { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ExpectativaVida> ExpectativasVida { get; set; }
        public DbSet<Limite> Limites { get; set; }
        public DbSet<IndiceCorrecao> IndicesCorrecao { get; set; }
        public DbSet<ValorIndiceCorrecao> ValoresIndiceCorrecao { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ReajusteRMI> ReajusteRMIs { get; set; }
        public DbSet<Simulacao> Simulacoes { get; set; }
        public DbSet<TempoContribuicao> TempoContribuicoes{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=VM-DESENV\VANGUARDA;Database=ReportServer$VANGUARDA;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ValorIndiceCorrecao>()
                .HasKey(c => new { c.Data, c.IndiceCorrecaoID });
        }
        public VanguardaContext(DbContextOptions<VanguardaContext> options) : base(options)
        {

        }
    }
}
