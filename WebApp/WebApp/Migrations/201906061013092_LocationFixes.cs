namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationFixes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GeoCoords", "NetworkLine_Id", "dbo.NetworkLines");
            DropForeignKey("dbo.Stations", "Coordinate_Id", "dbo.GeoCoords");
            DropIndex("dbo.GeoCoords", new[] { "NetworkLine_Id" });
            DropIndex("dbo.Stations", new[] { "Coordinate_Id" });
            AddColumn("dbo.Stations", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Stations", "Latitude", c => c.Double(nullable: false));
            DropColumn("dbo.Stations", "Coordinate_Id");
            DropTable("dbo.GeoCoords");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GeoCoords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        NetworkLine_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Stations", "Coordinate_Id", c => c.Int());
            DropColumn("dbo.Stations", "Latitude");
            DropColumn("dbo.Stations", "Longitude");
            CreateIndex("dbo.Stations", "Coordinate_Id");
            CreateIndex("dbo.GeoCoords", "NetworkLine_Id");
            AddForeignKey("dbo.Stations", "Coordinate_Id", "dbo.GeoCoords", "Id");
            AddForeignKey("dbo.GeoCoords", "NetworkLine_Id", "dbo.NetworkLines", "Id");
        }
    }
}
