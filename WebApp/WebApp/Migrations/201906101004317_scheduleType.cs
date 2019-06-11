namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scheduleType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        NetworkLine_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NetworkLines", t => t.NetworkLine_Id)
                .Index(t => t.NetworkLine_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "NetworkLine_Id", "dbo.NetworkLines");
            DropIndex("dbo.Schedules", new[] { "NetworkLine_Id" });
            DropTable("dbo.Schedules");
        }
    }
}
