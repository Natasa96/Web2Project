namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartureTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        NetworkLine_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NetworkLines", t => t.NetworkLine_Id)
                .Index(t => t.NetworkLine_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departures", "NetworkLine_Id", "dbo.NetworkLines");
            DropIndex("dbo.Departures", new[] { "NetworkLine_Id" });
            DropTable("dbo.Departures");
        }
    }
}
