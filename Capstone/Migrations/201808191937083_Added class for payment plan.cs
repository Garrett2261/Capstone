namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedclassforpaymentplan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        CardHolderName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentPlans");
        }
    }
}
