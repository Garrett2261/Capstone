namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAvailabilityandStartTimeandEndTimetotheEmployeeClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmployeeSchedules", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.EmployeeSchedules", new[] { "EmployeeId" });
            AddColumn("dbo.Employees", "Availability", c => c.String());
            AddColumn("dbo.Employees", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Employees", "PhoneNumber", c => c.Double(nullable: false));
            DropColumn("dbo.EmployeeSchedules", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeSchedules", "EmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "PhoneNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "EndTime");
            DropColumn("dbo.Employees", "StartTime");
            DropColumn("dbo.Employees", "Availability");
            CreateIndex("dbo.EmployeeSchedules", "EmployeeId");
            AddForeignKey("dbo.EmployeeSchedules", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
    }
}
