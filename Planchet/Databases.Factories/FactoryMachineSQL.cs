using Infrastructure.IServices;
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
    /// Creates a database associated to this machine
    /// </summary>
    public class FactoryMachineSQL : FactoryDbBase, IFactoryDB, IDisposable
    {
        IDatabaseInitializer<DataBaseSQL> inicializer;
        IIdentifierMachineService identifierMachineService;

        public FactoryMachineSQL(IDatabaseInitializer<DataBaseSQL> inicializer, IIdentifierMachineService identifierMachineService)
        {
            this.inicializer = inicializer;
            this.identifierMachineService=identifierMachineService;
        }

        /// <summary>
        /// Stores the connection string and instantiates a new database with the right connection string
        /// </summary>
        protected override void AsignDatabaseProperties(string connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                connectionString = this.ConcatIdentifier(System.Configuration.ConfigurationManager.ConnectionStrings["DatabasePlanchet"].ConnectionString);
                database = new DataBaseSQL(new SqlConnection(connectionString),inicializer);
            }
            else
            {
                ConnectionStringOld = connectionString + this.GetIdentifier();
                database = new DataBaseSQL(new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=" + ConnectionStringOld + ";Integrated Security=True;Persist Security Info=False;"), inicializer);
            }
            
        }

        /// <summary>
        /// Concat identifier of this machine to database name
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private string ConcatIdentifier(string connectionString)
        {
            string catalog = "Initial Catalog=";
            int startDatabaseIndex = connectionString.IndexOf(catalog) + catalog.Length;
            int finalDatabaseIndex = connectionString.IndexOf(";", startDatabaseIndex);
            return connectionString.Insert(finalDatabaseIndex, this.GetIdentifier());
        }

        /// <summary>
        /// Returns identifier of this machine
        /// </summary>
        /// <returns></returns>
        private string GetIdentifier()
        {
            return string.Concat("-", this.identifierMachineService.GetUniqueIdentifier());
        }
    
    }
}

 
