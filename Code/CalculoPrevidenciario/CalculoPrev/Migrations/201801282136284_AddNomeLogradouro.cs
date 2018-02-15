namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNomeLogradouro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Endereco", "NomeLogradouro", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Endereco", "NomeLogradouro");
        }
    }
}
