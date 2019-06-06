namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoCoords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NetworkLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LineNumber = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Timetable_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Timetables", t => t.Timetable_Id)
                .Index(t => t.Timetable_Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Coordinate_Id = c.Int(),
                        NLine_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeoCoords", t => t.Coordinate_Id)
                .ForeignKey("dbo.NetworkLines", t => t.NLine_Id)
                .Index(t => t.Coordinate_Id)
                .Index(t => t.NLine_Id);
            
            CreateTable(
                "dbo.Pricelists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pricelist_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pricelists", t => t.Pricelist_Id)
                .Index(t => t.Pricelist_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                        ValidationTime = c.DateTime(nullable: false),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Timetables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Birthdate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Lastname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Document", c => c.String());
            AddColumn("dbo.AspNetUsers", "Type", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Validation", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.NetworkLines", "Timetable_Id", "dbo.Timetables");
            DropForeignKey("dbo.TicketPrices", "Pricelist_Id", "dbo.Pricelists");
            DropForeignKey("dbo.Stations", "NLine_Id", "dbo.NetworkLines");
            DropForeignKey("dbo.Stations", "Coordinate_Id", "dbo.GeoCoords");
            DropIndex("dbo.TicketPrices", new[] { "Pricelist_Id" });
            DropIndex("dbo.Stations", new[] { "NLine_Id" });
            DropIndex("dbo.Stations", new[] { "Coordinate_Id" });
            DropIndex("dbo.NetworkLines", new[] { "Timetable_Id" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "Validation");
            DropColumn("dbo.AspNetUsers", "Type");
            DropColumn("dbo.AspNetUsers", "Document");
            DropColumn("dbo.AspNetUsers", "Lastname");
            DropColumn("dbo.AspNetUsers", "Firstname");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Birthdate");
            DropTable("dbo.Timetables");
            DropTable("dbo.Tickets");
            DropTable("dbo.TicketPrices");
            DropTable("dbo.Pricelists");
            DropTable("dbo.Stations");
            DropTable("dbo.NetworkLines");
            DropTable("dbo.GeoCoords");
        }
    }
}
