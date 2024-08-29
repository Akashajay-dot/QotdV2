using System.Data.Entity.Migrations;

namespace QOTD.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<QOTD.Web.Models.AppdbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QOTD.Web.Models.AppdbContext context)
        {
           
        }
    }
}
