namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoIC2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IndiceCorrecao", "Periodicidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IndiceCorrecao", "Periodicidade", c => c.Int(nullable: false));
        }
    }
}
