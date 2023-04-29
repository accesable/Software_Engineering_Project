namespace MobilePhoneDistributor_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        AgentId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 200),
                        PasswordSalt = c.String(maxLength: 100),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AgentId)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "dbo.DeliveryNotes",
                c => new
                    {
                        DeliveryNoteId = c.String(nullable: false, maxLength: 128),
                        DeliveryDate = c.DateTime(nullable: false),
                        OrderId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.DeliveryNoteId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 128),
                        OrderDate = c.DateTime(nullable: false),
                        OrderStatus = c.String(nullable: false, maxLength: 250),
                        PaymentMethod = c.String(nullable: false, maxLength: 250),
                        PaymentStatus = c.String(nullable: false, maxLength: 250),
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
            
            CreateTable(
                "dbo.PhoneModels",
                c => new
                    {
                        PhoneId = c.String(nullable: false, maxLength: 128),
                        PhoneName = c.String(nullable: false, maxLength: 100),
                        PhoneBrand = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PhoneId);
            
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        ReceiptId = c.String(nullable: false, maxLength: 128),
                        ReceiptDate = c.DateTime(nullable: false),
                        StaffId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ReceiptId)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.ReceiptDetails",
                c => new
                    {
                        ReceiptDetailId = c.Int(nullable: false, identity: true),
                        ReceiptId = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        PhoneModelId = c.String(nullable: false, maxLength: 128),
                        UnitAmmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ReceiptDetailId)
                .ForeignKey("dbo.PhoneModels", t => t.PhoneModelId, cascadeDelete: true)
                .ForeignKey("dbo.Receipts", t => t.ReceiptId, cascadeDelete: true)
                .Index(t => t.ReceiptId)
                .Index(t => t.PhoneModelId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 200),
                        PasswordSalt = c.String(maxLength: 100),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StaffId)
                .Index(t => t.Username, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receipts", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.ReceiptDetails", "ReceiptId", "dbo.Receipts");
            DropForeignKey("dbo.ReceiptDetails", "PhoneModelId", "dbo.PhoneModels");
            DropForeignKey("dbo.DeliveryNotes", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "PhoneModelId", "dbo.PhoneModels");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "AgentId", "dbo.Agents");
            DropIndex("dbo.Staffs", new[] { "Username" });
            DropIndex("dbo.ReceiptDetails", new[] { "PhoneModelId" });
            DropIndex("dbo.ReceiptDetails", new[] { "ReceiptId" });
            DropIndex("dbo.Receipts", new[] { "StaffId" });
            DropIndex("dbo.OrderDetails", new[] { "PhoneModelId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "AgentId" });
            DropIndex("dbo.DeliveryNotes", new[] { "OrderId" });
            DropIndex("dbo.Agents", new[] { "Username" });
            DropTable("dbo.Staffs");
            DropTable("dbo.ReceiptDetails");
            DropTable("dbo.Receipts");
            DropTable("dbo.PhoneModels");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.DeliveryNotes");
            DropTable("dbo.Agents");
        }
    }
}
