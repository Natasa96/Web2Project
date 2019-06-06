namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class coulmnfixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GeoCoords", "LinesId", c => c.Int());
            CreateIndex("dbo.GeoCoords", "LinesId");
            AddForeignKey("dbo.GeoCoords", "LinesId", "dbo.NetworkLines", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.GeoCoords", "NetworkLine_Id", "dbo.NetworkLines");
            DropIndex("dbo.GeoCoords", new[] { "NetworkLine_Id" });
            DropColumn("dbo.GeoCoords", "NetworkLine_Id");
        }
    }
}
