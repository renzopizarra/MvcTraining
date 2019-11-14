namespace DemoApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlengthEmployeeFirstname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String());
        }
    }
}
