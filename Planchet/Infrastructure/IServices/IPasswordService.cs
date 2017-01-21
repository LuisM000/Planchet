using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IServices
{
    /// <summary>
    /// In charge of management of passwords
    /// </summary>
    public interface IPasswordService
    {
        /// <summary>
        /// Generates new key
        /// </summary>
        /// <param name="key"></param>
        void CreateKey(string key);

        /// <summary>
        /// Cheks if the password is correct
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        bool IsCorrect(string password);
    }
}
