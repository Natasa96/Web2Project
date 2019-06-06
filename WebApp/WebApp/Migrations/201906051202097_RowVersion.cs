namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RowVersion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stations", "NLine_Id", "dbo.NetworkLines");
            DropIndex("dbo.Stations", new[] { "NLine_Id" });
            CreateTable(
                "dbo.StationNetworkLines",
                c => new
                {
                    Station_Id = c.Int(nullable: false),
                    NetworkLine_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Station_Id, t.NetworkLine_Id })
                .ForeignKey("dbo.Stations", t => t.Station_Id, cascadeDelete: true)
                .ForeignKey("dbo.NetworkLines", t => t.NetworkLine_Id, cascadeDelete: true)
                .Index(t => t.Station_Id)
                .Index(t => t.NetworkLine_Id);

            AddColumn("dbo.GeoCoords", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.NetworkLines", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Stations", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.AspNetUsers", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Pricelists", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.TicketPrices", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Tickets", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Timetables", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            DropColumn("dbo.Stations", "NLine_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.Stations", "NLine_Id", c => c.Int());
            DropForeignKey("dbo.StationNetworkLines", "NetworkLine_Id", "dbo.NetworkLines");
            DropForeignKey("dbo.StationNetworkLines", "Station_Id", "dbo.Stations");
            DropIndex("dbo.StationNetworkLines", new[] { "NetworkLine_Id" });
            DropIndex("dbo.StationNetworkLines", new[] { "Station_Id" });
            DropColumn("dbo.Timetables", "RowVersion");
            DropColumn("dbo.Tickets", "RowVersion");
            DropColumn("dbo.TicketPrices", "RowVersion");
            DropColumn("dbo.Pricelists", "RowVersion");
            DropColumn("dbo.AspNetUsers", "RowVersion");
            DropColumn("dbo.Stations", "RowVersion");
            DropColumn("dbo.NetworkLines", "RowVersion");
            DropColumn("dbo.GeoCoords", "RowVersion");
            DropTable("dbo.StationNetworkLines");
            CreateIndex("dbo.Stations", "NLine_Id");
            AddForeignKey("dbo.Stations", "NLine_Id", "dbo.NetworkLines", "Id");
        }
    }
}
