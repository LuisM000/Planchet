using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Factories
{
    /// <summary>
    /// Factory to create database Sql Express
    /// </summary>
    public class FactorySQL : FactoryDbBase, IFactoryDB, IDisposable
    {
         IDatabaseInitializer<DataBaseSQL> inicializer;

        public FactorySQL(IDatabaseInitializer<DataBaseSQL> inicializer)
        {
            this.inicializer = inicializer;
        }

        /// <summary>
        /// Stores the connection string and instantiates a new database with the right connection string
        /// </summary>
        protected override void AsignDatabaseProperties(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
                database = new DataBaseSQL(inicializer);
            else
            {
                ConnectionStringOld = connectionString;
                database = new DataBaseSQL(new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=" + connectionString + ";" + "Integrated Security=True;Persist Security Info=False;"), inicializer);
            }
        }
    
    }
}
