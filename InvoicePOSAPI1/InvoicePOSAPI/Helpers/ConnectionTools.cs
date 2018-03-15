using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace InvoicePOSAPI.Helpers
{
    public static class ConnectionTools
    {
        // all params are optional
        public static void ChangeDatabase(
            DbContext source,
            string initialCatalog = "",
            string dataSource = "",
            string userId = "",
            string password = "",
            bool integratedSecuity = true,
            string configConnectionStringName = "")
        
        {
            try
            {
                
                var configNameEf = string.IsNullOrEmpty(configConnectionStringName)
                    ? source.GetType().Name
                    : configConnectionStringName;

                // add a reference to System.Configuration
                var entityCnxStringBuilder = new EntityConnectionStringBuilder
                    (System.Configuration.ConfigurationManager
                        .ConnectionStrings[configNameEf].ConnectionString);

                // init the sqlbuilder with the full EF connectionstring cargo
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder
                    (entityCnxStringBuilder.ProviderConnectionString);

                // only populate parameters with values if added
                if (!string.IsNullOrEmpty(initialCatalog))
                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
                if (!string.IsNullOrEmpty(dataSource))
                    sqlCnxStringBuilder.DataSource = dataSource;
                if (!string.IsNullOrEmpty(userId))
                    sqlCnxStringBuilder.UserID = userId;
                if (!string.IsNullOrEmpty(password))
                    sqlCnxStringBuilder.Password = password;

                // set the integrated security status
                sqlCnxStringBuilder.IntegratedSecurity = integratedSecuity;

                // now flip the properties that were changed
                source.Database.Connection.ConnectionString
                    = sqlCnxStringBuilder.ConnectionString;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public static void changeToLocalDB(DbContext source)
        {
            ConnectionTools.ChangeDatabase( source,
                                            initialCatalog: "NEW_POS_DB",
                                            userId: "makrov",
                                            password: "Qsefthuk786",
                                            dataSource: @"localhost");
        }

        public static void ChangeToRemoteDB(DbContext source)
        {
            ConnectionTools.ChangeDatabase(source,
                                            initialCatalog: "NEW_POS_DB",
                                            userId: "makrov",
                                            password: "Qsefthuk786",
                                            dataSource: @"34.209.147.13");
        }
    }

}
