using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data.SqlServerCe;
using System.Collections.Generic;
using InvoicePOS.Views.Main;
using System.Windows.Threading;
using InvoicePOS.ViewModels;
using System.ComponentModel;

namespace InvoicePOS.Helpers
{
    public static class DBSynchronizer
    {
        public static void Synchronize(string serverConnectionString, string clientConnectionString, object sender)
        {
            try
            {
                
                // create a connection to the SyncCompactDB database
                SqlConnection clientConn = new SqlConnection(clientConnectionString);

                // create a connection to the SyncDB server database
                SqlConnection serverConn = new SqlConnection(serverConnectionString);

                //Get all table names from server
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = serverConn;

                serverConn.Open();

                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.


                List<string> tables = new List<string>();
                while (reader.Read())
                {
                    string tablename = reader["TABLE_NAME"].ToString();
                    tables.Add(tablename);
                }

                serverConn.Close();

                //provisioning
                DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription("tableSync");
                foreach (string name in tables)
                {
                    // get the description of the table name
                    // and add the table description to the sync scope definition
                    DbSyncTableDescription tableDescription = SqlSyncDescriptionBuilder.GetDescriptionForTable(name, serverConn);
                    scopeDesc.Tables.Add(tableDescription);
                }
                

                SqlSyncScopeProvisioning serverConfig = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
                serverConfig.SetCreateTableDefault(DbSyncCreationOption.CreateOrUseExisting);
                serverConfig.ObjectSchema = "dbo";
                serverConfig.CommandTimeout = 120;
                serverConfig.Apply();
                 
                //System.Threading.Thread.Sleep(2000);
                (sender as BackgroundWorker).ReportProgress(25);

                
                SqlSyncScopeProvisioning clientConfig = new SqlSyncScopeProvisioning(clientConn, scopeDesc);
                clientConfig.ObjectSchema = "dbo";
                clientConfig.SetCreateTableDefault(DbSyncCreationOption.CreateOrUseExisting);
                clientConfig.CommandTimeout = 120;
                clientConfig.Apply();
                
                //System.Threading.Thread.Sleep(1110);
                (sender as BackgroundWorker).ReportProgress(35);
                /////////////
            
                // create the sync orhcestrator
                SyncOrchestrator syncOrchestrator = new SyncOrchestrator();

                // set local provider of orchestrator to a CE sync provider associated with the 
                // ProductsScope in the SyncCompactDB compact client database
                syncOrchestrator.LocalProvider = new SqlSyncProvider("tableSync", clientConn);

                // set the remote provider of orchestrator to a server sync provider associated with
                // the ProductsScope in the SyncDB server database
                syncOrchestrator.RemoteProvider = new SqlSyncProvider("tableSync", serverConn);

                // set the direction of sync session to Upload and Download
                syncOrchestrator.Direction = SyncDirectionOrder.DownloadAndUpload;

                // subscribe for errors that occur when applying changes to the client
                ((SqlSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);

                // execute the synchronization process
                SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();
              
             
                //System.Threading.Thread.Sleep(2500);
                (sender as BackgroundWorker).ReportProgress(90);

            }
            catch (Exception e)
            {
                if (e.Message == "Could not create a scope with name 'tableSync' as a scope with that name already exists.")
                {
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                Deprovision(serverConnectionString, clientConnectionString);
                //System.Threading.Thread.Sleep(2500);
                (sender as BackgroundWorker).ReportProgress(100);
            }

        }


        static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {

            if (e.Conflict.Type == DbConflictType.LocalInsertRemoteInsert)
            {
                //e.Action = ApplyAction.RetryWithForceWrite;
                e.Action = ApplyAction.Continue;
            }
            else
            {
               throw e.Error;
            }
        }


        public static void Deprovision(string serverConnectionString, string clientConnectionString)
        {
            // Connection to  SQL Server database
            SqlConnection serverConn = new SqlConnection(serverConnectionString);

            // Connection to SQL client database
            SqlConnection clientConn = new SqlConnection(clientConnectionString);

            // Create Scope Deprovisioning for Sql Server and SQL client.
            SqlSyncScopeDeprovisioning serverSqlDepro = new SqlSyncScopeDeprovisioning(serverConn);
            SqlSyncScopeDeprovisioning clientSqlDepro = new SqlSyncScopeDeprovisioning(clientConn);

            //increase time out
            serverSqlDepro.CommandTimeout = 120;
            clientSqlDepro.CommandTimeout = 120;
            // Remove the scope from SQL Server remove all synchronization objects.
            serverSqlDepro.DeprovisionScope("tableSync");
            serverSqlDepro.DeprovisionStore();

            // Remove the scope from SQL client and remove all synchronization objects.
            clientSqlDepro.DeprovisionScope("tableSync");
            clientSqlDepro.DeprovisionStore();

            // Shut down database connections.
            serverConn.Close();
            serverConn.Dispose();
            clientConn.Close();
            clientConn.Dispose();

        }
    }
}