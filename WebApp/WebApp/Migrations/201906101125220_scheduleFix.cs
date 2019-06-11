namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scheduleFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timetables", "TTDay", c => c.Int(nullable: false));
            DropColumn("dbo.Timetables", "Day");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Timetables", "Day", c => c.DateTime());
            DropColumn("dbo.Timetables", "TTDay");
        }
    }
}
