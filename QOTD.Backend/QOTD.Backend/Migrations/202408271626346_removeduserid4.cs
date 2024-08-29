using System;
using System.Data.Entity.Migrations;

namespace QOTD.Web.Migrations
{
    public partial class removeduserid4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReputationMasters", "Badge", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.ReputationMasters", "Badge");
        }
    }
}
