namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificacaoPessoaEndereco : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pessoa", "EscritorioID", "dbo.Escritorio");
            DropForeignKey("dbo.ValorIndiceCorrecao", "IndiceCorrecaoID", "dbo.IndiceCorrecao");
            DropForeignKey("dbo.Pessoa", "EnderecoID", "dbo.Endereco");
            RenameColumn(table: "dbo.Pessoa", name: "EnderecoID", newName: "EnderecoCobrancaID");
            RenameIndex(table: "dbo.Pessoa", name: "IX_EnderecoID", newName: "IX_EnderecoCobrancaID");
            AddColumn("dbo.Pessoa", "Sexo", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "Tratamento", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "EstadoCivil", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "Profissao", c => c.String());
            AddColumn("dbo.Pessoa", "DocumentoIdentificacao", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "NumeroDocumentoIdentificacao", c => c.String());
            AddColumn("dbo.Pessoa", "Telefone1", c => c.String());
            AddColumn("dbo.Pessoa", "Telefone2", c => c.String());
            AddColumn("dbo.Pessoa", "Telefone3", c => c.String());
            AddColumn("dbo.Pessoa", "EnderecoCorrespondenciaID", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "EnderecoFaturamentoID", c => c.Int(nullable: false));
            AddColumn("dbo.Endereco", "Telefone", c => c.String());
            CreateIndex("dbo.Pessoa", "EnderecoCorrespondenciaID");
            CreateIndex("dbo.Pessoa", "EnderecoFaturamentoID");
            AddForeignKey("dbo.Pessoa", "EnderecoCorrespondenciaID", "dbo.Endereco", "EnderecoID");
            AddForeignKey("dbo.Pessoa", "EnderecoFaturamentoID", "dbo.Endereco", "EnderecoID");
            AddForeignKey("dbo.Pessoa", "EscritorioID", "dbo.Escritorio", "EscritorioID");
            AddForeignKey("dbo.ValorIndiceCorrecao", "IndiceCorrecaoID", "dbo.IndiceCorrecao", "IndiceCorrecaoID");
            AddForeignKey("dbo.Pessoa", "EnderecoCobrancaID", "dbo.Endereco", "EnderecoID");
            DropColumn("dbo.Pessoa", "Telefone");
            DropColumn("dbo.Pessoa", "Identidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoa", "Identidade", c => c.String());
            AddColumn("dbo.Pessoa", "Telefone", c => c.String());
            DropForeignKey("dbo.Pessoa", "EnderecoCobrancaID", "dbo.Endereco");
            DropForeignKey("dbo.ValorIndiceCorrecao", "IndiceCorrecaoID", "dbo.IndiceCorrecao");
            DropForeignKey("dbo.Pessoa", "EscritorioID", "dbo.Escritorio");
            DropForeignKey("dbo.Pessoa", "EnderecoFaturamentoID", "dbo.Endereco");
            DropForeignKey("dbo.Pessoa", "EnderecoCorrespondenciaID", "dbo.Endereco");
            DropIndex("dbo.Pessoa", new[] { "EnderecoFaturamentoID" });
            DropIndex("dbo.Pessoa", new[] { "EnderecoCorrespondenciaID" });
            DropColumn("dbo.Endereco", "Telefone");
            DropColumn("dbo.Pessoa", "EnderecoFaturamentoID");
            DropColumn("dbo.Pessoa", "EnderecoCorrespondenciaID");
            DropColumn("dbo.Pessoa", "Telefone3");
            DropColumn("dbo.Pessoa", "Telefone2");
            DropColumn("dbo.Pessoa", "Telefone1");
            DropColumn("dbo.Pessoa", "NumeroDocumentoIdentificacao");
            DropColumn("dbo.Pessoa", "DocumentoIdentificacao");
            DropColumn("dbo.Pessoa", "Profissao");
            DropColumn("dbo.Pessoa", "EstadoCivil");
            DropColumn("dbo.Pessoa", "Tratamento");
            DropColumn("dbo.Pessoa", "Sexo");
            RenameIndex(table: "dbo.Pessoa", name: "IX_EnderecoCobrancaID", newName: "IX_EnderecoID");
            RenameColumn(table: "dbo.Pessoa", name: "EnderecoCobrancaID", newName: "EnderecoID");
            AddForeignKey("dbo.Pessoa", "EnderecoID", "dbo.Endereco", "EnderecoID", cascadeDelete: true);
            AddForeignKey("dbo.ValorIndiceCorrecao", "IndiceCorrecaoID", "dbo.IndiceCorrecao", "IndiceCorrecaoID", cascadeDelete: true);
            AddForeignKey("dbo.Pessoa", "EscritorioID", "dbo.Escritorio", "EscritorioID", cascadeDelete: true);
        }
    }
}
