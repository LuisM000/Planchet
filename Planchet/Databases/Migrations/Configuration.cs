using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Databases.Migrations
{
    public class Configuration : DbMigrationsConfiguration<DataBaseSQL>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

   
    }
}
