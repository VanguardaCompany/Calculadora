namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClienteColaboradorEndereco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        EnderecoID = c.Int(nullable: false, identity: true),
                        Local = c.Int(nullable: false),
                        Logradouro = c.Int(nullable: false),
                        Numero = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        Pais = c.String(),
                        Cep = c.String(),
                    })
                .PrimaryKey(t => t.EnderecoID);
            
            AddColumn("dbo.Pessoa", "EnderecoID", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "InscricaoINSS", c => c.String());
            AddColumn("dbo.Pessoa", "Nit", c => c.String());
            AddColumn("dbo.Pessoa", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Pessoa", "EnderecoID");
            AddForeignKey("dbo.Pessoa", "EnderecoID", "dbo.Endereco", "EnderecoID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pessoa", "EnderecoID", "dbo.Endereco");
            DropIndex("dbo.Pessoa", new[] { "EnderecoID" });
            DropColumn("dbo.Pessoa", "Discriminator");
            DropColumn("dbo.Pessoa", "Nit");
            DropColumn("dbo.Pessoa", "InscricaoINSS");
            DropColumn("dbo.Pessoa", "EnderecoID");
            DropTable("dbo.Endereco");
        }
    }
}
