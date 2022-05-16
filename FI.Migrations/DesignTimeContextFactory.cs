using FI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;

namespace FI.Migrations
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<FIContext>
    {
        private const string LocalSql = "server=DESKTOP-ETKL9R5\\SQLEXPRESS;database=FI-Local;Trusted_Connection=True;";

        private static readonly string MigrationAssemblyName = typeof(DesignTimeContextFactory).Assembly.GetName().Name;

        public FIContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FIContext>()
                .UseSqlServer(args.FirstOrDefault() ?? LocalSql);
            return new FIContext(builder.Options);
        }
    }
}
