namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Passenger_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tickets", "Passenger_Id");
            AddForeignKey("dbo.Tickets", "Passenger_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "Passenger_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tickets", new[] { "Passenger_Id" });
            DropColumn("dbo.Tickets", "Passenger_Id");
        }
    }
}
