namespace CalculoPrev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReajusteRMI1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReajusteRMI",
                c => new
                    {
                        ReajusteRMIID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        ReajusteTotal = c.Double(nullable: false),
                        Fator1 = c.Double(nullable: false),
                        Fator2 = c.Double(nullable: false),
                        Fator3 = c.Double(nullable: false),
                        Fator4 = c.Double(nullable: false),
                        Fator5 = c.Double(nullable: false),
                        Fator6 = c.Double(nullable: false),
                        Fator7 = c.Double(nullable: false),
                        Fator8 = c.Double(nullable: false),
                        Fator9 = c.Double(nullable: false),
                        Fator10 = c.Double(nullable: false),
                        Fator11 = c.Double(nullable: false),
                        Fator12 = c.Double(nullable: false),
                        linkPrevidencia = c.String(),
                    })
                .PrimaryKey(t => t.ReajusteRMIID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReajusteRMI");
        }
    }
}
