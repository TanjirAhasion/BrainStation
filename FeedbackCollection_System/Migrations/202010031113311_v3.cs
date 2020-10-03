namespace FeedbackCollection_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "CreatedBy", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "CreatedBy", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "CreatedBy");
            CreateIndex("dbo.Posts", "CreatedBy");
            AddForeignKey("dbo.Posts", "CreatedBy", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "CreatedBy", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "CreatedBy" });
            DropIndex("dbo.Comments", new[] { "CreatedBy" });
            AlterColumn("dbo.Posts", "CreatedBy", c => c.String());
            AlterColumn("dbo.Comments", "CreatedBy", c => c.String());
        }
    }
}
