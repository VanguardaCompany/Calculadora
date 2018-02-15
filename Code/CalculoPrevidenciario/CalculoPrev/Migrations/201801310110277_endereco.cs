namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endereco : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "EnderecoID", c => c.Int(nullable: false));
            AddColumn("dbo.Endereco", "Local", c => c.Int(nullable: false));
            AddColumn("dbo.Endereco", "Logradouro", c => c.Int(nullable: false));
            AddColumn("dbo.Endereco", "NomeLogradouro", c => c.String());
            AddColumn("dbo.Endereco", "Numero", c => c.String());
            AddColumn("dbo.Endereco", "Complemento", c => c.String());
            AddColumn("dbo.Endereco", "Bairro", c => c.String());
            AddColumn("dbo.Endereco", "Cidade", c => c.String());
            AddColumn("dbo.Endereco", "Estado", c => c.String());
            AddColumn("dbo.Endereco", "Pais", c => c.String());
            AddColumn("dbo.Endereco", "Cep", c => c.String());
            AddColumn("dbo.Endereco", "Contato", c => c.String());
            AddColumn("dbo.Endereco", "Telefone", c => c.String());
            CreateIndex("dbo.Pessoa", "EnderecoID");
            AddForeignKey("dbo.Pessoa", "EnderecoID", "dbo.Endereco", "EnderecoID");
            DropColumn("dbo.Pessoa", "Local");
            DropColumn("dbo.Pessoa", "Logradouro");
            DropColumn("dbo.Pessoa", "NomeLogradouro");
            DropColumn("dbo.Pessoa", "Numero");
            DropColumn("dbo.Pessoa", "Complemento");
            DropColumn("dbo.Pessoa", "Bairro");
            DropColumn("dbo.Pessoa", "Cidade");
            DropColumn("dbo.Pessoa", "Estado");
            DropColumn("dbo.Pessoa", "Pais");
            DropColumn("dbo.Pessoa", "Cep");
            DropColumn("dbo.Pessoa", "Contato");
            DropColumn("dbo.Pessoa", "Telefone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoa", "Telefone", c => c.String());
            AddColumn("dbo.Pessoa", "Contato", c => c.String());
            AddColumn("dbo.Pessoa", "Cep", c => c.String());
            AddColumn("dbo.Pessoa", "Pais", c => c.String());
            AddColumn("dbo.Pessoa", "Estado", c => c.String());
            AddColumn("dbo.Pessoa", "Cidade", c => c.String());
            AddColumn("dbo.Pessoa", "Bairro", c => c.String());
            AddColumn("dbo.Pessoa", "Complemento", c => c.String());
            AddColumn("dbo.Pessoa", "Numero", c => c.String());
            AddColumn("dbo.Pessoa", "NomeLogradouro", c => c.String());
            AddColumn("dbo.Pessoa", "Logradouro", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "Local", c => c.Int(nullable: false));
            DropForeignKey("dbo.Pessoa", "EnderecoID", "dbo.Endereco");
            DropIndex("dbo.Pessoa", new[] { "EnderecoID" });
            DropColumn("dbo.Endereco", "Telefone");
            DropColumn("dbo.Endereco", "Contato");
            DropColumn("dbo.Endereco", "Cep");
            DropColumn("dbo.Endereco", "Pais");
            DropColumn("dbo.Endereco", "Estado");
            DropColumn("dbo.Endereco", "Cidade");
            DropColumn("dbo.Endereco", "Bairro");
            DropColumn("dbo.Endereco", "Complemento");
            DropColumn("dbo.Endereco", "Numero");
            DropColumn("dbo.Endereco", "NomeLogradouro");
            DropColumn("dbo.Endereco", "Logradouro");
            DropColumn("dbo.Endereco", "Local");
            DropColumn("dbo.Pessoa", "EnderecoID");
        }
    }
}
