namespace StudentManagementSystems.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Quantity");
        }
    }
}
