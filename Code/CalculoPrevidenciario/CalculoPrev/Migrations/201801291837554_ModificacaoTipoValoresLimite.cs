namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificacaoTipoValoresLimite : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Limite", "TetoContribuicao", c => c.Double(nullable: false));
            AlterColumn("dbo.Limite", "TetoRMI", c => c.Double(nullable: false));
            AlterColumn("dbo.Limite", "MenorValorTeto", c => c.Double(nullable: false));
            AlterColumn("dbo.Limite", "MaiorValorTeto", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Limite", "MaiorValorTeto", c => c.Int(nullable: false));
            AlterColumn("dbo.Limite", "MenorValorTeto", c => c.Int(nullable: false));
            AlterColumn("dbo.Limite", "TetoRMI", c => c.Int(nullable: false));
            AlterColumn("dbo.Limite", "TetoContribuicao", c => c.Int(nullable: false));
        }
    }
}
