namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profile4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Pic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Pic");
        }
    }
}
