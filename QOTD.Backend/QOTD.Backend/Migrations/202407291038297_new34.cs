namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new34 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "QuestionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "QuestionDate", c => c.DateTime(nullable: false));
        }
    }
}
