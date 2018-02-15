namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndiceCorrecao", "Readonly", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IndiceCorrecao", "Readonly");
        }
    }
}
