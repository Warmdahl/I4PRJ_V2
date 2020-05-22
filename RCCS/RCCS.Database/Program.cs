using System;
using RCCS.Database.Data;

namespace RCCS.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] jsonPath = { "/../../../../RCCS.DatabaseAPI/appsettings.json" };
            DesignTimeRCCSContextFactory rccsContextFactory = new DesignTimeRCCSContextFactory();
            using RCCSContext context = rccsContextFactory.CreateDbContext(jsonPath);
            DataSeeder dataSeeder = new DataSeeder(context);
            dataSeeder.SeedData1();
            dataSeeder.SeedData2();
            dataSeeder.SeedRespiteCareHomeData();
        }
    }
}