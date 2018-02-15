namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificacaoEnderecoEmailTelefone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Endereco", "Estado", c => c.String());
            AddColumn("dbo.Endereco", "Contato", c => c.String());
            AddColumn("dbo.Pessoa", "Email", c => c.String());
            AddColumn("dbo.Pessoa", "HomePage", c => c.String());
            AddColumn("dbo.Pessoa", "TipoTelefone1", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "TipoTelefone2", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "TipoTelefone3", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "TipoTelefone3");
            DropColumn("dbo.Pessoa", "TipoTelefone2");
            DropColumn("dbo.Pessoa", "TipoTelefone1");
            DropColumn("dbo.Pessoa", "HomePage");
            DropColumn("dbo.Pessoa", "Email");
            DropColumn("dbo.Endereco", "Contato");
            DropColumn("dbo.Endereco", "Estado");
        }
    }
}
