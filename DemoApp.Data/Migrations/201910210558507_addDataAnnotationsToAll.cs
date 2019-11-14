namespace DemoApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataAnnotationsToAll : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false, maxLength: 225));
            AlterColumn("dbo.Employees", "LastName", c => c.String(nullable: false, maxLength: 225));
            AlterColumn("dbo.Employees", "ContactNumber", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 225));
            AlterColumn("dbo.Employees", "Address", c => c.String(nullable: false, maxLength: 225));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Address", c => c.String());
            AlterColumn("dbo.Employees", "Email", c => c.String());
            AlterColumn("dbo.Employees", "ContactNumber", c => c.String());
            AlterColumn("dbo.Employees", "LastName", c => c.String());
            AlterColumn("dbo.Employees", "FirstName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
