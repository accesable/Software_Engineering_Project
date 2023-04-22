namespace MobilePhoneDistributor_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueUsername : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Staffs", new[] { "Username" });
            CreateIndex("dbo.Staffs", "Username", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Staffs", new[] { "Username" });
            CreateIndex("dbo.Staffs", "Username");
        }
    }
}
