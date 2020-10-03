namespace FeedbackCollection_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commitee02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "Comments_CommentId", "dbo.Comments");
            DropIndex("dbo.Votes", new[] { "Comments_CommentId" });
            DropColumn("dbo.Votes", "CommentId");
            RenameColumn(table: "dbo.Votes", name: "Comments_CommentId", newName: "CommentId");
            AddColumn("dbo.Votes", "VoteType", c => c.Int(nullable: false));
            AlterColumn("dbo.Votes", "CommentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Votes", "CommentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "CommentId");
            AddForeignKey("dbo.Votes", "CommentId", "dbo.Comments", "CommentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "CommentId", "dbo.Comments");
            DropIndex("dbo.Votes", new[] { "CommentId" });
            AlterColumn("dbo.Votes", "CommentId", c => c.Int());
            AlterColumn("dbo.Votes", "CommentId", c => c.String());
            DropColumn("dbo.Votes", "VoteType");
            RenameColumn(table: "dbo.Votes", name: "CommentId", newName: "Comments_CommentId");
            AddColumn("dbo.Votes", "CommentId", c => c.String());
            CreateIndex("dbo.Votes", "Comments_CommentId");
            AddForeignKey("dbo.Votes", "Comments_CommentId", "dbo.Comments", "CommentId");
        }
    }
}
