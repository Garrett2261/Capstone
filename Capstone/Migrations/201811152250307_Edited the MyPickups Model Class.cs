namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedtheMyPickupsModelClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyPickups", "DayOfTheWeek", c => c.String());
            DropColumn("dbo.MyPickups", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyPickups", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.MyPickups", "DayOfTheWeek");
        }
    }
}
