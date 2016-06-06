namespace Epica.Services.App_Start
{
    using Epica.Data;
    using Data.Migrations;
    using System.Data.Entity;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EpicaDbContext,Configuration>());
            EpicaDbContext.Create().Database.Initialize(true);
        }
    }
}