namespace FeedbackCollection_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commitee01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Votes", "Comments_CommentId", c => c.Int());
            CreateIndex("dbo.Votes", "Comments_CommentId");
            AddForeignKey("dbo.Votes", "Comments_CommentId", "dbo.Comments", "CommentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Comments_CommentId", "dbo.Comments");
            DropIndex("dbo.Votes", new[] { "Comments_CommentId" });
            DropColumn("dbo.Votes", "Comments_CommentId");
        }
    }
}
