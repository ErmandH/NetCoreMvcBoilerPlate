using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDSC.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GDSC.DataAccessLayer
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GdscDbContext>
    {
        public GdscDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<GdscDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(CustomConfigurationProvider.GetRemoteConnectionString());
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
