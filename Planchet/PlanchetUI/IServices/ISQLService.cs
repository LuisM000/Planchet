using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanchetUI.IServices
{
    /// <summary>
    /// Basic service to access to server
    /// </summary>
    public interface ISQLService
    {
        /// <summary>
        /// Returns all databases from connection string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IList<string> GetDatabases(string connectionString);
    }
}
