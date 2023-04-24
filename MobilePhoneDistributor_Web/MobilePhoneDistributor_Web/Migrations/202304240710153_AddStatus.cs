namespace MobilePhoneDistributor_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        DeliveryStatus = c.String(nullable: false, maxLength: 250),
                        PaymentStatus = c.String(nullable: false, maxLength: 50),
                        PaymentMethod = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.StatusId);
            
            AddColumn("dbo.Orders", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "StatusId");
            AddForeignKey("dbo.Orders", "StatusId", "dbo.OrderStatus", "StatusId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "StatusId", "dbo.OrderStatus");
            DropIndex("dbo.Orders", new[] { "StatusId" });
            DropColumn("dbo.Orders", "StatusId");
            DropTable("dbo.OrderStatus");
        }
    }
}
