namespace MobilePhoneDistributor_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 128),
                        OrderDate = c.DateTime(nullable: false),
                        AgentId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Agents", t => t.AgentId, cascadeDelete: true)
                .Index(t => t.AgentId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        PhoneModelId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.PhoneModels", t => t.PhoneModelId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.PhoneModelId);
            
            AlterColumn("dbo.ReceiptDetails", "UnitAmmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "PhoneModelId", "dbo.PhoneModels");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "AgentId", "dbo.Agents");
            DropIndex("dbo.OrderDetails", new[] { "PhoneModelId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "AgentId" });
            AlterColumn("dbo.ReceiptDetails", "UnitAmmount", c => c.String(nullable: false, maxLength: 50));
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
        }
    }
}
