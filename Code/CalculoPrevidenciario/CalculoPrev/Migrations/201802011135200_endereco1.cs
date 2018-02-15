namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class endereco1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Pessoa", "Telefone1", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "Telefone1", c => c.String());
            AlterColumn("dbo.Pessoa", "Nome", c => c.String());
        }
    }
}
