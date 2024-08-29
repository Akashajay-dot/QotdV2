namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new32 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "QuestionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "QuestionDate", c => c.DateTime(nullable: false));
        }
    }
}
