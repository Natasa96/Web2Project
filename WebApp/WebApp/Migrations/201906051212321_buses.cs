namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class buses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GeoCoords", "NetworkLine_Id", c => c.Int());
            CreateIndex("dbo.GeoCoords", "NetworkLine_Id");
            AddForeignKey("dbo.GeoCoords", "NetworkLine_Id", "dbo.NetworkLines", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeoCoords", "NetworkLine_Id", "dbo.NetworkLines");
            DropIndex("dbo.GeoCoords", new[] { "NetworkLine_Id" });
            DropColumn("dbo.GeoCoords", "NetworkLine_Id");
        }
    }
}
