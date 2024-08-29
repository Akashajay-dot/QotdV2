namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Questions", "CorrectMessage");
            DropColumn("dbo.Questions", "WrongMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "WrongMessage", c => c.String());
            AddColumn("dbo.Questions", "CorrectMessage", c => c.String());
            DropColumn("dbo.Questions", "IsActive");
        }
    }
}
