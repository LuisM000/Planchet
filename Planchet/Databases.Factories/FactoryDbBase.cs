using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Factories
{
    /// <summary>
    /// Factory to create database context
    /// </summary>
    public abstract class FactoryDbBase : IFactoryDB, IDisposable
    {
        protected DataBase database;
        protected String ConnectionStringOld;

        public virtual void Dispose()
        {
            if (database != null) database.Dispose();
        }

        /// <summary>
        /// Creates an returns a database. If the database with the same connectionstring has already been created, 
        /// it returns the already created 
        /// </summary>
        /// <returns>New or existent Database</returns>
        public DataBase CreateDataBase(string connectionString)
        {
            //If the conection string hasn't changed I don't create a new database. Just return the already existent
            if (ConnectionStringOld != connectionString || database == null)
            {
                if (database != null) database.Dispose();
                AsignDatabaseProperties(connectionString);
            }
            return database;
        }

        /// <summary>
        /// Creates an returns a database. This database is always new
        /// </summary>
        /// <returns>new database</returns>
        public DataBase CreateNewDataBase(string connectionString)
        {
            if(database!=null)
                database.Dispose();
            AsignDatabaseProperties(connectionString);
            return database;
        }

        protected abstract void AsignDatabaseProperties(string connectionString);
    }
}
