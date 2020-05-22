using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/*
 * Code adapted from: https://gist.github.com/Rudyzio/dd1c565d58d955b09620d2201a7d5710#file-applicationdbcontext-cs
 */

namespace RCCS.Database.Data
{
    class DesignTimeRCCSContextFactory : IDesignTimeDbContextFactory<RCCSContext>
    {
        public RCCSContext CreateDbContext(string[] args)
        {
            IConfiguration configuration 
                = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + (args.Length == 0 ? "/../RCCS.DatabaseAPI/appsettings.json" : args[0]))
                    .Build();
            var optionsBuilder = new DbContextOptionsBuilder<RCCSContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new RCCSContext(optionsBuilder.Options);
        }
    }
}