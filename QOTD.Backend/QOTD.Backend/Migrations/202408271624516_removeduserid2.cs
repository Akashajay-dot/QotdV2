namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeduserid2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReputationMasters", "MinPoints", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReputationMasters", "MinPoints");
        }
    }
}
