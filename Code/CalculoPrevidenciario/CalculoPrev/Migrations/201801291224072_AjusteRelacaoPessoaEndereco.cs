namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteRelacaoPessoaEndereco : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pessoa", "EnderecoCobrancaID", "dbo.Endereco");
            DropForeignKey("dbo.Pessoa", "EnderecoCorrespondenciaID", "dbo.Endereco");
            DropForeignKey("dbo.Pessoa", "EnderecoFaturamentoID", "dbo.Endereco");
            DropIndex("dbo.Pessoa", new[] { "EnderecoCorrespondenciaID" });
            DropIndex("dbo.Pessoa", new[] { "EnderecoCobrancaID" });
            DropIndex("dbo.Pessoa", new[] { "EnderecoFaturamentoID" });
            AddColumn("dbo.Pessoa", "Local", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "Logradouro", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "NomeLogradouro", c => c.String());
            AddColumn("dbo.Pessoa", "Numero", c => c.String());
            AddColumn("dbo.Pessoa", "Complemento", c => c.String());
            AddColumn("dbo.Pessoa", "Bairro", c => c.String());
            AddColumn("dbo.Pessoa", "Cidade", c => c.String());
            AddColumn("dbo.Pessoa", "Estado", c => c.String());
            AddColumn("dbo.Pessoa", "Pais", c => c.String());
            AddColumn("dbo.Pessoa", "Cep", c => c.String());
            AddColumn("dbo.Pessoa", "Contato", c => c.String());
            AddColumn("dbo.Pessoa", "Telefone", c => c.String());
            DropColumn("dbo.Endereco", "Local");
            DropColumn("dbo.Endereco", "Logradouro");
            DropColumn("dbo.Endereco", "NomeLogradouro");
            DropColumn("dbo.Endereco", "Numero");
            DropColumn("dbo.Endereco", "Complemento");
            DropColumn("dbo.Endereco", "Bairro");
            DropColumn("dbo.Endereco", "Cidade");
            DropColumn("dbo.Endereco", "Estado");
            DropColumn("dbo.Endereco", "Pais");
            DropColumn("dbo.Endereco", "Cep");
            DropColumn("dbo.Endereco", "Contato");
            DropColumn("dbo.Endereco", "Telefone");
            DropColumn("dbo.Pessoa", "EnderecoCorrespondenciaID");
            DropColumn("dbo.Pessoa", "EnderecoCobrancaID");
            DropColumn("dbo.Pessoa", "EnderecoFaturamentoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoa", "EnderecoFaturamentoID", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "EnderecoCobrancaID", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "EnderecoCorrespondenciaID", c => c.Int(nullable: false));
            AddColumn("dbo.Endereco", "Telefone", c => c.String());
            AddColumn("dbo.Endereco", "Contato", c => c.String());
            AddColumn("dbo.Endereco", "Cep", c => c.String());
            AddColumn("dbo.Endereco", "Pais", c => c.String());
            AddColumn("dbo.Endereco", "Estado", c => c.String());
            AddColumn("dbo.Endereco", "Cidade", c => c.String());
            AddColumn("dbo.Endereco", "Bairro", c => c.String());
            AddColumn("dbo.Endereco", "Complemento", c => c.String());
            AddColumn("dbo.Endereco", "Numero", c => c.String());
            AddColumn("dbo.Endereco", "NomeLogradouro", c => c.String());
            AddColumn("dbo.Endereco", "Logradouro", c => c.Int(nullable: false));
            AddColumn("dbo.Endereco", "Local", c => c.Int(nullable: false));
            DropColumn("dbo.Pessoa", "Telefone");
            DropColumn("dbo.Pessoa", "Contato");
            DropColumn("dbo.Pessoa", "Cep");
            DropColumn("dbo.Pessoa", "Pais");
            DropColumn("dbo.Pessoa", "Estado");
            DropColumn("dbo.Pessoa", "Cidade");
            DropColumn("dbo.Pessoa", "Bairro");
            DropColumn("dbo.Pessoa", "Complemento");
            DropColumn("dbo.Pessoa", "Numero");
            DropColumn("dbo.Pessoa", "NomeLogradouro");
            DropColumn("dbo.Pessoa", "Logradouro");
            DropColumn("dbo.Pessoa", "Local");
            CreateIndex("dbo.Pessoa", "EnderecoFaturamentoID");
            CreateIndex("dbo.Pessoa", "EnderecoCobrancaID");
            CreateIndex("dbo.Pessoa", "EnderecoCorrespondenciaID");
            AddForeignKey("dbo.Pessoa", "EnderecoFaturamentoID", "dbo.Endereco", "EnderecoID");
            AddForeignKey("dbo.Pessoa", "EnderecoCorrespondenciaID", "dbo.Endereco", "EnderecoID");
            AddForeignKey("dbo.Pessoa", "EnderecoCobrancaID", "dbo.Endereco", "EnderecoID");
        }
    }
}
