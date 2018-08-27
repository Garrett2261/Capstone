namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedthecustomermodelandchangedtheVetAddresscolumntoVetHospitalNameandaddedacolumnVetHospitalAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "VetHospitalName", c => c.String());
            AddColumn("dbo.Customers", "VetHospitalAddress", c => c.String());
            DropColumn("dbo.Customers", "VetAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "VetAddress", c => c.String());
            DropColumn("dbo.Customers", "VetHospitalAddress");
            DropColumn("dbo.Customers", "VetHospitalName");
        }
    }
}
