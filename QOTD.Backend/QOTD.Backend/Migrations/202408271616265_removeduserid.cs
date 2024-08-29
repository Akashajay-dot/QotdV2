namespace QOTD.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeduserid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReputationMasters", "UserId", "dbo.Users");
            DropIndex("dbo.ReputationMasters", new[] { "UserId" });
            DropColumn("dbo.ReputationMasters", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReputationMasters", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReputationMasters", "UserId");
            AddForeignKey("dbo.ReputationMasters", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
