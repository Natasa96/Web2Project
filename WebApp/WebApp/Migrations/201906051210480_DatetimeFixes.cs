namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class DatetimeFixes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pricelists", "StartTime", c => c.DateTime());
            AlterColumn("dbo.Pricelists", "EndTime", c => c.DateTime());
            AlterColumn("dbo.Tickets", "ValidationTime", c => c.DateTime());
            AlterColumn("dbo.Timetables", "Day", c => c.DateTime());
        }

        public override void Down()
        {
            AlterColumn("dbo.Timetables", "Day", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tickets", "ValidationTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pricelists", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pricelists", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
