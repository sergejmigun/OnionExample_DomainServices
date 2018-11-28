using System;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using Microsoft.SqlServer.Dac;
using System.Collections.Generic;

namespace OnionExample.Domain.DataProviders.Helpers
{
    internal static class MsSqlHelper
    {
        public static bool CheckDatabaseExists(string databaseName)
        {
            using (var connection = new SqlConnection(GetConnectionString(true)))
            {
                using (var command = new SqlCommand($"SELECT db_id('{databaseName}')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }

        public static void DeployDbInstance(string databaseName)
        {
            var dbServices = new DacServices(GetConnectionString(true));
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/bin"), "OnionExample.Database.dacpac");

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var dbPackage = DacPackage.Load(fs, DacSchemaModelStorageType.Memory, FileAccess.Read);

                dbServices.Deploy(dbPackage, databaseName, options: new DacDeployOptions
                {
                    CreateNewDatabase = true
                });
            }
        }

        public static string GetConnectionString(bool serverOnlyConnectionString = false)
        {
            string connString = ConfigurationManager.ConnectionStrings["OnionExample"].ConnectionString;

            if (serverOnlyConnectionString)
            {
                var values = connString.Split(';').Where(x => !string.IsNullOrWhiteSpace(x)).ToDictionary(x => x.Split('=')[0].ToLower().Trim(), x => x.Split('=')[1]);
                var server = values["data source"];

                values.Remove("initial catalog");
                values.Remove("data source");

                values.Add("server", server);

                return string.Join(";", values.Select(x => x.Key + "=" + x.Value));
            }

            return connString;
        }
    }
}
