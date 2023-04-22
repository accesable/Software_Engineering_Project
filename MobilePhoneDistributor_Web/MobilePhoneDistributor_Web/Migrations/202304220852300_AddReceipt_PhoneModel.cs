namespace MobilePhoneDistributor_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceipt_PhoneModel : DbMigration
    {
        public override void Up()
        {
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
                        ReceiptDetailId = c.String(nullable: false, maxLength: 128),
                        ReceiptId = c.String(nullable: false, maxLength: 128),
                        PhoneId = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        UnitAmmount = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ReceiptDetailId)
                .ForeignKey("dbo.PhoneModels", t => t.PhoneId, cascadeDelete: true)
                .ForeignKey("dbo.Receipts", t => t.ReceiptId, cascadeDelete: true)
                .Index(t => t.ReceiptId)
                .Index(t => t.PhoneId);
            
            CreateTable(
                "dbo.PhoneModels",
                c => new
                    {
                        PhoneId = c.String(nullable: false, maxLength: 128),
                        PhoneName = c.String(nullable: false, maxLength: 100),
                        PhoneBrand = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PhoneId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receipts", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.ReceiptDetails", "ReceiptId", "dbo.Receipts");
            DropForeignKey("dbo.ReceiptDetails", "PhoneId", "dbo.PhoneModels");
            DropIndex("dbo.ReceiptDetails", new[] { "PhoneId" });
            DropIndex("dbo.ReceiptDetails", new[] { "ReceiptId" });
            DropIndex("dbo.Receipts", new[] { "StaffId" });
            DropTable("dbo.PhoneModels");
            DropTable("dbo.ReceiptDetails");
            DropTable("dbo.Receipts");
        }
    }
}
