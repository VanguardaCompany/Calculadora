namespace CalculoPrev.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Escritorio",
                c => new
                    {
                        EscritorioID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Endereco = c.String(),
                        Telefone = c.String(),
                        Registro = c.String(),
                    })
                .PrimaryKey(t => t.EscritorioID);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        PessoaID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Telefone = c.String(),
                        Cpf = c.String(),
                        Identidade = c.String(),
                        Nascimento = c.DateTime(nullable: false),
                        EscritorioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PessoaID)
                .ForeignKey("dbo.Escritorio", t => t.EscritorioID, cascadeDelete: true)
                .Index(t => t.EscritorioID);
            
            CreateTable(
                "dbo.ExpectativaVida",
                c => new
                    {
                        ExpectativaVidaID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Valor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExpectativaVidaID);
            
            CreateTable(
                "dbo.IndiceCorrecao",
                c => new
                    {
                        IndiceCorrecaoID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Periodicidade = c.Int(nullable: false),
                        Tipo = c.Int(nullable: false),
                        DataInicio = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                        UltimoValor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IndiceCorrecaoID);
            
            CreateTable(
                "dbo.Limite",
                c => new
                    {
                        LimiteID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        TetoContribuicao = c.Int(nullable: false),
                        TetoRMI = c.Int(nullable: false),
                        MenorValorTeto = c.Int(nullable: false),
                        MaiorValorTeto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LimiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pessoa", "EscritorioID", "dbo.Escritorio");
            DropIndex("dbo.Pessoa", new[] { "EscritorioID" });
            DropTable("dbo.Limite");
            DropTable("dbo.IndiceCorrecao");
            DropTable("dbo.ExpectativaVida");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Escritorio");
        }
    }
}
