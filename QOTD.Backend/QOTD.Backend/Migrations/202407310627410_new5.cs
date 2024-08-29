namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Points", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Points");
        }
    }
}
