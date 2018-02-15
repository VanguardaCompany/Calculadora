using CalculoPrev.DAL.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CalculoPrev.DAL.DAL
{
    public class Vanguardacontext : DbContext
    {
        public Vanguardacontext() : base("VanguardaConnectionstring")
        {

        }
        public DbSet<Escritorio> Escritorios { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ExpectativaVida> ExpectativasVida { get; set; }
        public DbSet<Limite> Limites { get; set; }
        public DbSet<IndiceCorrecao> IndicesCorrecao { get; set; }
        public DbSet<ValorIndiceCorrecao> ValoresIndiceCorrecao { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<ReajusteRMI> ReajusteRMIs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            //modelBuilder.Entity<Endereco>()
            //    .HasRequired(endereco => endereco.Pessoa)
                //.WithOptional(pessoa => pessoa.EnderecoCorrespondencia);

            base.OnModelCreating(modelBuilder);
            
            //modelBuilder.Entity<Pessoa>()
            //.HasOne(p => p.Escritorios)
            //.WithMany(b => b.Posts)
            //.HasForeignKey(p => p.BlogId)
            //.HasConstraintName("ForeignKey_Post_Blog");
        }

        
    }
}