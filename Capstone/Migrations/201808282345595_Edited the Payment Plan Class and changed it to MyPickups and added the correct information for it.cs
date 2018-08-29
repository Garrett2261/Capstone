namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedthePaymentPlanClassandchangedittoMyPickupsandaddedthecorrectinformationforit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyPickups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DogId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Frequency = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dogs", t => t.DogId, cascadeDelete: true)
                .Index(t => t.DogId);
            
            DropTable("dbo.PaymentPlans");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(nullable: false),
                        Amount = c.Double(nullable: false),
                        CardHolderName = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        AddressCity = c.String(),
                        AddressPostcode = c.String(),
                        AddressCountry = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.MyPickups", "DogId", "dbo.Dogs");
            DropIndex("dbo.MyPickups", new[] { "DogId" });
            DropTable("dbo.MyPickups");
        }
    }
}
