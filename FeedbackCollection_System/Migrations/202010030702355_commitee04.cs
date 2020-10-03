namespace FeedbackCollection_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commitee04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            DropColumn("dbo.AspNetUsers", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MyProperty", c => c.String());
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
