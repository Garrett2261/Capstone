namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedtheMyPickupsclassbutkepttheTimepropertywhichIkeptandgotridoftheDayOfTheWeekproperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MyPickups", "DayOfTheWeek");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyPickups", "DayOfTheWeek", c => c.String());
        }
    }
}
