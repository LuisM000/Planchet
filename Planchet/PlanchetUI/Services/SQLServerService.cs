using PlanchetUI.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanchetUI.Services
{
    /// <summary>
    /// Basic service to access to SQL server
    /// </summary>
    public class SQLServerService : ISQLService
    {
        /// <summary>
        /// Returns all databases (Sql server) from connection string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public IList<string> GetDatabases(string connectionString)
        {
            List<string> databases = new List<string>();
            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                using (System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand("SELECT name from sys.databases", connection))
                    using (System.Data.IDataReader dr = sqlCommand.ExecuteReader())
                        while (dr.Read())
                            databases.Add(dr[0].ToString());
            }
            return databases;
        }
    }
}
