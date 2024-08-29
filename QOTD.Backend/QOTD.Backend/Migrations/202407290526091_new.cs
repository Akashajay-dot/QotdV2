namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "GoogleId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "GoogleId", c => c.Int(nullable: false));
        }
    }
}
