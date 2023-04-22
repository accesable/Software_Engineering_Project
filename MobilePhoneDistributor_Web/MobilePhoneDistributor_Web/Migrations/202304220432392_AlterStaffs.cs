namespace MobilePhoneDistributor_Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterStaffs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "Password", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Staffs", "PasswordSalt", c => c.String(maxLength: 100));
            AddColumn("dbo.Staffs", "PhoneNumber", c => c.String(nullable: false));
            CreateIndex("dbo.Staffs", "Username");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Staffs", new[] { "Username" });
            DropColumn("dbo.Staffs", "PhoneNumber");
            DropColumn("dbo.Staffs", "PasswordSalt");
            DropColumn("dbo.Staffs", "Password");
        }
    }
}
