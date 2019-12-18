namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paypal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaypalCredentials",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PurchaseUnit = c.String(),
                        FullName = c.String(),
                        Address = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        Ticket_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.Ticket_Id)
                .Index(t => t.Ticket_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaypalCredentials", "Ticket_Id", "dbo.Tickets");
            DropIndex("dbo.PaypalCredentials", new[] { "Ticket_Id" });
            DropTable("dbo.PaypalCredentials");
        }
    }
}
