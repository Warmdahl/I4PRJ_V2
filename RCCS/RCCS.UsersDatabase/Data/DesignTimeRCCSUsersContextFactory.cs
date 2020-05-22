/*
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/*
 * Code adapted from: https://gist.github.com/Rudyzio/dd1c565d58d955b09620d2201a7d5710#file-applicationdbcontext-cs
 #1#

namespace RCCS.DatabaseUsers.Data
{
    class DesignTimeRCCSUsersContextFactory : IDesignTimeDbContextFactory<RCCSUsersContext>
    {
        public RCCSUsersContext CreateDbContext(string[] args)
        {
            IConfiguration configuration
                = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + (args.Length == 0 ? "/../RCCS.DatabaseAPI/appsettings.json" : args[0]))
                    .Build();
            var optionsBuilder = new DbContextOptionsBuilder<RCCSUsersContext>();
            var connectionString = configuration.GetConnectionString("UsersConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new RCCSUsersContext(optionsBuilder.Options);
        }
    }
}
*/
