namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedtheMyPickupsmodelandaddedaTimepropertythatusestheDateTimeclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyPickups", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyPickups", "Time");
        }
    }
}
