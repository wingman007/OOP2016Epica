namespace Epica.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Epica.Data;

    public sealed class Configuration : DbMigrationsConfiguration<EpicaDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }
}
