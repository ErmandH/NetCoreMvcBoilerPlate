using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GDSC.DataAccessLayer
{
    public class CustomConfigurationProvider
    {


        public static string? GetLocalConnectionString()
        {

            ConfigurationManager configurationManager = new();
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("LocalConnection");
        }

        public static string? GetRemoteConnectionString()
        {

            ConfigurationManager configurationManager = new();
            //configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../GDSC.AdminPanel"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("RemoteConnection");
        }
    }
}
