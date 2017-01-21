using Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    /// <summary>
    /// Service in charge of managing credentials
    /// </summary>
    public interface ICredentialsService
    {
        /// <summary>
        /// Saves credentials
        /// </summary>
        /// <param name="credential"></param>
        /// <returns>Indicates if the credential has been correctly stored</returns>
        bool CreateCredentials(Credential credential);

        /// <summary>
        /// Checks if the credential is correct
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        bool IsCorrect(Credential credential);
    }
}
