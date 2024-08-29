namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedNewSchema2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerKeys",
                c => new
                    {
                        AnswerKeyId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        AnswerOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerKeyId)
                .ForeignKey("dbo.AnswerOptions", t => t.AnswerOptionId, cascadeDelete: true)
                .Index(t => t.AnswerOptionId);
            
            CreateTable(
                "dbo.AnswerOptions",
                c => new
                    {
                        AnswerOptionId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Option = c.String(),
                    })
                .PrimaryKey(t => t.AnswerOptionId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        CategoryId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        CorrectMessage = c.String(),
                        WrongMessage = c.String(),
                        Point = c.Int(nullable: false),
                        HasMultipleAnswers = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        QuestionDate = c.DateTime(nullable: true),
                        SnapShot = c.String(),
                        LastUpdatedOn = c.DateTime(nullable: false),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GoogleId = c.Int(nullable: false),
                        CreatedON = c.DateTime(nullable: false),
                        UpdatedON = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ReputationMasters",
                c => new
                    {
                        ReputationMasterId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UptoPoints = c.Int(nullable: false),
                        Badge = c.String(),
                    })
                .PrimaryKey(t => t.ReputationMasterId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReputationMasters", "UserId", "dbo.Users");
            DropForeignKey("dbo.AnswerKeys", "AnswerOptionId", "dbo.AnswerOptions");
            DropForeignKey("dbo.AnswerOptions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Questions", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ReputationMasters", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "AuthorId" });
            DropIndex("dbo.Questions", new[] { "CategoryId" });
            DropIndex("dbo.AnswerOptions", new[] { "QuestionId" });
            DropIndex("dbo.AnswerKeys", new[] { "AnswerOptionId" });
            DropTable("dbo.ReputationMasters");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Questions");
            DropTable("dbo.AnswerOptions");
            DropTable("dbo.AnswerKeys");
        }
    }
}
