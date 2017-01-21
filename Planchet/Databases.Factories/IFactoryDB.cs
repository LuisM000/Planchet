using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Factories
{
    /// <summary>
    /// Represents a database creator
    /// </summary>
    public interface IFactoryDB
    {
        /// <summary>
        /// Creates an returns a database. If the database with the same connectionstring has already been created, it returns the already created 
        /// </summary>
        /// <returns>New or existent Database</returns>
        DataBase CreateDataBase(string connectionString);

        /// <summary>
        /// Creates an returns a database. This database is always new
        /// </summary>
        /// <returns>new database</returns>
        DataBase CreateNewDataBase(string connectionString);
    }
}
