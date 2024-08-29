namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeduserid3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReputationMasters", "Badge");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReputationMasters", "Badge", c => c.String());
        }
    }
}
