using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Databases
{
    /// <summary>
    /// Represents a database sql 
    /// </summary>
    public class DataBaseSQL:DataBase
    {
        static string connection;
        public DataBaseSQL(SqlConnection connectionString, IDatabaseInitializer<DataBaseSQL> initializer)
            : base(connectionString)
        {
            connection = this.Database.Connection.ConnectionString;
            Database.SetInitializer(initializer);
        }

        /// <summary>
        /// If a connection String is not provided, the system try to use the defined in app config
        /// </summary>
        /// <param name="initializer"></param>
        public DataBaseSQL(IDatabaseInitializer<DataBaseSQL> initializer)
            : base(new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabasePlanchet"].ConnectionString))
        {
            connection = this.Database.Connection.ConnectionString;
            Database.SetInitializer(initializer);
        }

        /// <summary>
        /// Is necessary to migrations
        /// </summary>
        public DataBaseSQL()
            : base(new SqlConnection(connection))
        {
        }
    }
}
