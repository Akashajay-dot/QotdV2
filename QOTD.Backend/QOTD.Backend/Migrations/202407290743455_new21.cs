namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserResponses",
                c => new
                    {
                        UserResponseId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AnswerOptionId = c.Int(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserResponseId)
                .ForeignKey("dbo.AnswerOptions", t => t.AnswerOptionId, cascadeDelete: false)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId)
                .Index(t => t.AnswerOptionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserResponses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserResponses", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.UserResponses", "AnswerOptionId", "dbo.AnswerOptions");
            DropIndex("dbo.UserResponses", new[] { "AnswerOptionId" });
            DropIndex("dbo.UserResponses", new[] { "UserId" });
            DropIndex("dbo.UserResponses", new[] { "QuestionId" });
            DropTable("dbo.UserResponses");
        }
    }
}
