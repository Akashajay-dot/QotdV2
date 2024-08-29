namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedNewSchema3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AnswerKeys", "QuestionId");
            AddForeignKey("dbo.AnswerKeys", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswerKeys", "QuestionId", "dbo.Questions");
            DropIndex("dbo.AnswerKeys", new[] { "QuestionId" });
        }
    }
}
