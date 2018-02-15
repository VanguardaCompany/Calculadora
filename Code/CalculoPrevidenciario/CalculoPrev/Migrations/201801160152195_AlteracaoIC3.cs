namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoIC3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndiceCorrecao", "Periodicidade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IndiceCorrecao", "Periodicidade");
        }
    }
}
