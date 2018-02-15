namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInformacoesIC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndiceCorrecao", "Informacoes", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IndiceCorrecao", "Informacoes");
        }
    }
}
