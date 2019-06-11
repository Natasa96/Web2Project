namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimetableFK : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.NetworkLines", name: "Timetable_Id", newName: "TimeOfGoing_Id");
            RenameIndex(table: "dbo.NetworkLines", name: "IX_Timetable_Id", newName: "IX_TimeOfGoing_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.NetworkLines", name: "IX_TimeOfGoing_Id", newName: "IX_Timetable_Id");
            RenameColumn(table: "dbo.NetworkLines", name: "TimeOfGoing_Id", newName: "Timetable_Id");
        }
    }
}
