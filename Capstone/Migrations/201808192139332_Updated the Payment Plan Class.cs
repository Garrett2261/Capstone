namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedthePaymentPlanClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentPlans", "AddressLine1", c => c.String());
            AddColumn("dbo.PaymentPlans", "AddressLine2", c => c.String());
            AddColumn("dbo.PaymentPlans", "AddressCity", c => c.String());
            AddColumn("dbo.PaymentPlans", "AddressPostcode", c => c.String());
            AddColumn("dbo.PaymentPlans", "AddressCountry", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentPlans", "AddressCountry");
            DropColumn("dbo.PaymentPlans", "AddressPostcode");
            DropColumn("dbo.PaymentPlans", "AddressCity");
            DropColumn("dbo.PaymentPlans", "AddressLine2");
            DropColumn("dbo.PaymentPlans", "AddressLine1");
        }
    }
}
