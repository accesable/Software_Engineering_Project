namespace MobilePhoneDistributor_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeleveryNote : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeliveryNotes", "OrderId", "dbo.Orders");
            DropIndex("dbo.DeliveryNotes", new[] { "OrderId" });
            DropTable("dbo.DeliveryNotes");
        }
    }
}
