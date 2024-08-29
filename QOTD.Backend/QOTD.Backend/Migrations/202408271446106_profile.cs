namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "profilePic", c => c.String());
            DropColumn("dbo.Users", "GoogleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "GoogleId", c => c.String());
            DropColumn("dbo.Users", "profilePic");
        }
    }
}
