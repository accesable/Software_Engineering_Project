namespace MobilePhoneDistributor_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalCreate : DbMigration
    {
        public override void Up()
        {
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
                        ReceiptDetailId = c.String(nullable: false, maxLength: 128),
                        ReceiptId = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        PhoneModelId = c.String(nullable: false, maxLength: 128),
                        UnitAmmount = c.String(nullable: false, maxLength: 50),
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
            DropIndex("dbo.Staffs", new[] { "Username" });
            DropIndex("dbo.ReceiptDetails", new[] { "PhoneModelId" });
            DropIndex("dbo.ReceiptDetails", new[] { "ReceiptId" });
            DropIndex("dbo.Receipts", new[] { "StaffId" });
            DropTable("dbo.Staffs");
            DropTable("dbo.ReceiptDetails");
            DropTable("dbo.Receipts");
            DropTable("dbo.PhoneModels");
        }
    }
}
