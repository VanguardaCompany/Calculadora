namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoIC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndiceCorrecao", "Descricao", c => c.String(maxLength: 500));
            AddColumn("dbo.IndiceCorrecao", "Estado", c => c.Int(nullable: false));
            AlterColumn("dbo.IndiceCorrecao", "Nome", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IndiceCorrecao", "Nome", c => c.String());
            DropColumn("dbo.IndiceCorrecao", "Estado");
            DropColumn("dbo.IndiceCorrecao", "Descricao");
        }
    }
}
