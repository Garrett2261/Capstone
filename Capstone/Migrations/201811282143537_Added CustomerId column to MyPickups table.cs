namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomerIdcolumntoMyPickupstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyPickups", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.MyPickups", "CustomerId");
            AddForeignKey("dbo.MyPickups", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyPickups", "CustomerId", "dbo.Customers");
            DropIndex("dbo.MyPickups", new[] { "CustomerId" });
            DropColumn("dbo.MyPickups", "CustomerId");
        }
    }
}
