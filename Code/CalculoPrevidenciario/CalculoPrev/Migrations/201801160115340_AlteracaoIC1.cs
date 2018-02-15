namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoIC1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndiceCorrecao", "Periodicidade", c => c.Int(nullable: false));
            DropColumn("dbo.IndiceCorrecao", "Periodicidade_Codigo");
            DropColumn("dbo.IndiceCorrecao", "Periodicidade_Descricao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IndiceCorrecao", "Periodicidade_Descricao", c => c.String());
            AddColumn("dbo.IndiceCorrecao", "Periodicidade_Codigo", c => c.Int(nullable: false));
            DropColumn("dbo.IndiceCorrecao", "Periodicidade");
        }
    }
}
