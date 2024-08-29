namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "QuestionDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "QuestionDate");
        }
    }
}
