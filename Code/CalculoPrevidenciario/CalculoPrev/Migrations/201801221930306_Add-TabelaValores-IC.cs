namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTabelaValoresIC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValorIndiceCorrecao",
                c => new
                    {
                        Data = c.DateTime(nullable: false),
                        IndiceCorrecaoID = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.Data, t.IndiceCorrecaoID })
                .ForeignKey("dbo.IndiceCorrecao", t => t.IndiceCorrecaoID, cascadeDelete: true)
                .Index(t => t.IndiceCorrecaoID);
            
            DropColumn("dbo.IndiceCorrecao", "UltimoValor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IndiceCorrecao", "UltimoValor", c => c.Int(nullable: false));
            DropForeignKey("dbo.ValorIndiceCorrecao", "IndiceCorrecaoID", "dbo.IndiceCorrecao");
            DropIndex("dbo.ValorIndiceCorrecao", new[] { "IndiceCorrecaoID" });
            DropTable("dbo.ValorIndiceCorrecao");
        }
    }
}
