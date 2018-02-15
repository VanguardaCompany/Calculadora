namespace CalculoPrev.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancaModelo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        PessoaID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Sexo = c.Int(nullable: false),
                        Tratamento = c.Int(nullable: false),
                        EstadoCivil = c.Int(nullable: false),
                        Profissao = c.String(),
                        DocumentoIdentificacao = c.Int(nullable: false),
                        NumeroDocumentoIdentificacao = c.String(),
                        Email = c.String(),
                        HomePage = c.String(),
                        TipoTelefone1 = c.Int(nullable: false),
                        TipoTelefone2 = c.Int(nullable: false),
                        TipoTelefone3 = c.Int(nullable: false),
                        Telefone1 = c.String(nullable: false),
                        Telefone2 = c.String(),
                        Telefone3 = c.String(),
                        Cpf = c.String(nullable: false),
                        Nascimento = c.DateTime(nullable: false),
                        EnderecoID = c.Int(nullable: false),
                        EscritorioID = c.Int(nullable: false),
                        InscricaoINSS = c.String(),
                        Nit = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PessoaID)
                .ForeignKey("dbo.Endereco", t => t.EnderecoID)
                .ForeignKey("dbo.Escritorio", t => t.EscritorioID)
                .Index(t => t.EnderecoID)
                .Index(t => t.EscritorioID);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        EnderecoID = c.Int(nullable: false, identity: true),
                        Local = c.Int(nullable: false),
                        Logradouro = c.Int(nullable: false),
                        NomeLogradouro = c.String(),
                        Numero = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                        Pais = c.String(),
                        Cep = c.String(),
                        Contato = c.String(),
                        Telefone = c.String(),
                    })
                .PrimaryKey(t => t.EnderecoID);
            
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
                        Nome = c.String(maxLength: 150),
                        Periodicidade = c.Int(nullable: false),
                        Tipo = c.Int(nullable: false),
                        DataInicio = c.DateTime(nullable: false),
                        DataFim = c.DateTime(nullable: false),
                        Descricao = c.String(maxLength: 500),
                        Informacoes = c.String(maxLength: 500),
                        Extinto = c.Boolean(nullable: false),
                        Atualizado = c.Boolean(nullable: false),
                        Readonly = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IndiceCorrecaoID);
            
            CreateTable(
                "dbo.ValorIndiceCorrecao",
                c => new
                    {
                        Data = c.DateTime(nullable: false),
                        IndiceCorrecaoID = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.Data, t.IndiceCorrecaoID })
                .ForeignKey("dbo.IndiceCorrecao", t => t.IndiceCorrecaoID)
                .Index(t => t.IndiceCorrecaoID);
            
            CreateTable(
                "dbo.Limite",
                c => new
                    {
                        LimiteID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        TetoContribuicao = c.Double(nullable: false),
                        TetoRMI = c.Double(nullable: false),
                        MenorValorTeto = c.Double(nullable: false),
                        MaiorValorTeto = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LimiteID);
            
            CreateTable(
                "dbo.ReajusteRMI",
                c => new
                    {
                        ReajusteRMIID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        ReajusteTotal = c.Double(nullable: false),
                        Fator1 = c.Double(nullable: false),
                        Fator2 = c.Double(nullable: false),
                        Fator3 = c.Double(nullable: false),
                        Fator4 = c.Double(nullable: false),
                        Fator5 = c.Double(nullable: false),
                        Fator6 = c.Double(nullable: false),
                        Fator7 = c.Double(nullable: false),
                        Fator8 = c.Double(nullable: false),
                        Fator9 = c.Double(nullable: false),
                        Fator10 = c.Double(nullable: false),
                        Fator11 = c.Double(nullable: false),
                        Fator12 = c.Double(nullable: false),
                        linkPrevidencia = c.String(),
                    })
                .PrimaryKey(t => t.ReajusteRMIID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValorIndiceCorrecao", "IndiceCorrecaoID", "dbo.IndiceCorrecao");
            DropForeignKey("dbo.Pessoa", "EscritorioID", "dbo.Escritorio");
            DropForeignKey("dbo.Pessoa", "EnderecoID", "dbo.Endereco");
            DropIndex("dbo.ValorIndiceCorrecao", new[] { "IndiceCorrecaoID" });
            DropIndex("dbo.Pessoa", new[] { "EscritorioID" });
            DropIndex("dbo.Pessoa", new[] { "EnderecoID" });
            DropTable("dbo.ReajusteRMI");
            DropTable("dbo.Limite");
            DropTable("dbo.ValorIndiceCorrecao");
            DropTable("dbo.IndiceCorrecao");
            DropTable("dbo.ExpectativaVida");
            DropTable("dbo.Escritorio");
            DropTable("dbo.Endereco");
            DropTable("dbo.Pessoa");
        }
    }
}
