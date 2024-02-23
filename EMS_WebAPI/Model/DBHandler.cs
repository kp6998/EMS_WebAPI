using System;
using System.Data;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EMS_WebAPI.Model
{
    public class DBHandler
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        private static string dbRef = configuration.GetSection("ConnectionStrings")["SqliteConnection"];
        private static SQLiteConnection connection = new SQLiteConnection(dbRef);

        public static string ExecuteQuery(ref string strStatus, string query)
        {
            string result = null;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = query;
                if (query.ToUpper().Contains("SELECT"))
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    result = JsonConvert.SerializeObject(dt);
                }
                else
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        if (query.ToUpper().Contains("INSERT") || query.ToUpper().Contains("UPDATE"))
                        {
                            result = cmd.ExecuteNonQuery().ToString();
                        }
                        else
                        {
                            cmd.ExecuteNonQuery();
                            result = "Done";
                        }
                        transaction.Commit();
                    }
                }
                strStatus = "01";
            }
            catch (Exception ex)
            {
                strStatus = "00";
                result = "Error: " + ex.Message;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }

            return result;
        }
    }
}
