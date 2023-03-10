using Microsoft.Extensions.Configuration;
using System;

namespace OEPERU.Scheduler.Common.Configuration
{
    public class ConfigurationHelper
    {
        #region GetConfiguration()

        public static IConfigurationRoot GetConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            if (!String.IsNullOrWhiteSpace(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false);
            }
 
            return builder.Build();
        }
        #endregion
    }
}
