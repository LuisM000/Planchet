using PlanchetUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlanchetUI.Entities
{
    /// <summary>
    /// Describes data connection to database
    /// </summary>
    public class Connection:EntityBase
    {
        public Connection() { }

        private string dataSource;
        public string DataSource
        {
            get { return dataSource; }
            set { dataSource = value; OnPropertyChanged(()=>DataSource); }
        }
        
        private string initialCatalog;
        public string InitialCatalog
        {
            get { return initialCatalog; }
            set { initialCatalog = value; OnPropertyChanged(()=>InitialCatalog); }
        }
        
        private bool integratedSecurity;
        public bool IntegratedSecurity
        {
            get { return integratedSecurity; }
            set { integratedSecurity = value; OnPropertyChanged(()=>IntegratedSecurity); }
        }
        
        private string user;
        public string User
        {
            get { return user; }
            set { user = value; OnPropertyChanged(()=>User); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(()=>Password); }
        }

        private bool encrypt;
        public bool Encrypt
        {
            get { return encrypt; }
            set { encrypt = value; OnPropertyChanged(()=>Encrypt); }
        }

        public string ConnectionString
        {
            get { return this.GetConnectionString(); }
        }
        public string BasicConnectionString
        {
            get { return this.GetBasicConnectionString(); }
        }

        /// <summary>
        /// Creates Connection object from string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static Connection GetConnection(string connectionString)
        {
            Connection connection = new Connection();
            //DataSource
            connection.DataSource = GetKey(connectionString, "Data Source=");
            //Database
            connection.InitialCatalog = GetKey(connectionString, "Initial Catalog=");
            //Security
            string integratedSecurity = GetKey(connectionString, "Integrated Security=");
            if (!String.IsNullOrEmpty(integratedSecurity))
                connection.IntegratedSecurity = bool.Parse(integratedSecurity);
            if(!connection.IntegratedSecurity)
            {
                //User
                connection.User = GetKey(connectionString, "User ID=");
                //Password
                connection.Password = GetKey(connectionString, "Password=");
            }
            //Encrypt
            string encrypt = GetKey(connectionString, "Encrypt=");
            if (!String.IsNullOrEmpty(encrypt))
                connection.Encrypt = bool.Parse(encrypt);

            return connection;
        }
        /// <summary>
        /// Gets connection string from Connetion object
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            string connectionString = String.Empty;
            //Data source
            if (!String.IsNullOrEmpty(this.DataSource))
                connectionString += string.Concat("Data Source=", this.DataSource, ";");
            //DataBase
            if (!String.IsNullOrEmpty(this.InitialCatalog))
                connectionString += string.Concat("Initial Catalog=", this.InitialCatalog, ";");
            //Security
            connectionString += string.Concat("Integrated Security=", this.IntegratedSecurity.ToString(), ";");
            if(!this.IntegratedSecurity)
            {
                //User
                if (!String.IsNullOrEmpty(this.User))
                    connectionString += string.Concat("User ID=", this.User, ";");
                //Password
                if (!String.IsNullOrEmpty(this.Password))
                    connectionString += string.Concat("Password=", this.Password, ";");
            }
            //Encrypt
            connectionString += string.Concat("Encrypt=", this.Encrypt.ToString(), ";");
            return connectionString;
        }
        /// <summary>
        /// Gets basic connection string from Connection object
        /// </summary>
        /// <returns></returns>
        public string GetBasicConnectionString()
        {
            string connectionString = String.Empty;
            //Data source
            if (!String.IsNullOrEmpty(this.DataSource))
                connectionString += string.Concat("Data Source=", this.DataSource, ";");
            //Security
            connectionString += string.Concat("Integrated Security=", this.IntegratedSecurity.ToString(), ";");
            if (!this.IntegratedSecurity)
            {
                //User
                if (!String.IsNullOrEmpty(this.User))
                    connectionString += string.Concat("User ID=", this.User, ";");
                //Password
                if (!String.IsNullOrEmpty(this.Password))
                    connectionString += string.Concat("Password=", this.Password, ";");
            }

            return connectionString;
        }

        private static string GetKey(string connectionString, string key)
        {
            string value = String.Empty;
            int startIndex = GetStartIndex(connectionString, key);
            if (startIndex != -1)
            {
                int finalIndex = GetFinalIndex(connectionString, startIndex);
                value = connectionString.Substring(startIndex, finalIndex);
            }
            return value;
        }
        private static int GetStartIndex(string connectionString, string key)
        {
            int index = connectionString.IndexOf(key);
            if (index != -1)
                index += key.Length;
            return index;
        }
        private static int GetFinalIndex(string connectionString, int startIndex)
        {
            return connectionString.IndexOf(";", startIndex) - startIndex;
        }

        protected override void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            base.OnPropertyChanged(action);
            base.OnPropertyChanged(()=>ConnectionString);
            base.OnPropertyChanged(() => BasicConnectionString);
        }
 
    }
}
