namespace MobilePhoneDistributor_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceipt_PhoneModel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReceiptDetails", "PhoneId", "dbo.PhoneModels");
            DropIndex("dbo.ReceiptDetails", new[] { "PhoneId" });
            RenameColumn(table: "dbo.ReceiptDetails", name: "PhoneId", newName: "PhoneModel_PhoneId");
            AddColumn("dbo.ReceiptDetails", "PhoneModelId", c => c.String(nullable: false));
            AlterColumn("dbo.ReceiptDetails", "PhoneModel_PhoneId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ReceiptDetails", "PhoneModel_PhoneId");
            AddForeignKey("dbo.ReceiptDetails", "PhoneModel_PhoneId", "dbo.PhoneModels", "PhoneId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiptDetails", "PhoneModel_PhoneId", "dbo.PhoneModels");
            DropIndex("dbo.ReceiptDetails", new[] { "PhoneModel_PhoneId" });
            AlterColumn("dbo.ReceiptDetails", "PhoneModel_PhoneId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ReceiptDetails", "PhoneModelId");
            RenameColumn(table: "dbo.ReceiptDetails", name: "PhoneModel_PhoneId", newName: "PhoneId");
            CreateIndex("dbo.ReceiptDetails", "PhoneId");
            AddForeignKey("dbo.ReceiptDetails", "PhoneId", "dbo.PhoneModels", "PhoneId", cascadeDelete: true);
        }
    }
}
