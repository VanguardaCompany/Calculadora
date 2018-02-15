namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoIC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndiceCorrecao", "Extinto", c => c.Boolean(nullable: false));
            AddColumn("dbo.IndiceCorrecao", "Atualizado", c => c.Boolean(nullable: false));
            DropColumn("dbo.IndiceCorrecao", "Estado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IndiceCorrecao", "Estado", c => c.Int(nullable: false));
            DropColumn("dbo.IndiceCorrecao", "Atualizado");
            DropColumn("dbo.IndiceCorrecao", "Extinto");
        }
    }
}
