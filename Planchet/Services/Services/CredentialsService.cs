using Domain.Interfaces;
using Infrastructure.IServices;
using Model.Settings;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    /// <summary>
    /// Service in charge of managing credentials
    /// </summary>
    public class CredentialsService : ICredentialsService
    {
        IEncryptionService encryptionService;
        IRepository<Credential> repositoryCredential;

        public CredentialsService(IEncryptionService encryptionService,IRepository<Credential> repositoryCredential)
        {
            this.encryptionService = encryptionService;
            this.repositoryCredential = repositoryCredential;
        }

        /// <summary>
        /// Saves credentials
        /// </summary>
        /// <param name="credential"></param>
        /// <returns>Indicates if the credential has been correctly stored</returns>
        public bool CreateCredentials(Model.Settings.Credential credential)
        {
            //ToDo: not supports multiple credentials...Obviously (if supports multiple credentials)
            //should check that there is no equal user.
            //ToDo: there are not a minimum requiriments for user/password
            if (!this.repositoryCredential.GetAll().Any() && credential != null &&
                 !String.IsNullOrWhiteSpace(credential.User) && !String.IsNullOrWhiteSpace(credential.Password))
            {
                credential.Hash=this.GetHash(credential);
                //Saves credentials
                this.repositoryCredential.Insert(credential);
                int saveCredential = this.repositoryCredential.SaveChanges();
                if (saveCredential == 1)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the credential is correct
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        public bool IsCorrect(Model.Settings.Credential credential)
        {
            bool isCorrect = false;
            if(credential!=null && !string.IsNullOrEmpty(credential.Password))
            {
                Credential coincidence = this.repositoryCredential.GetAll().FirstOrDefault(c => string.Equals(c.User,credential.User));
                if(coincidence!=null && !string.IsNullOrEmpty(coincidence.Hash))
                {
                    //Compares the hash key of received credentials with the coincidant credential
                    string currentHash = this.GetHash(credential);
                    string coincidenceHash = coincidence.Hash;
                    isCorrect = string.Equals(currentHash, coincidenceHash);
                }
            }
            return isCorrect;
        }

        /// <summary>
        /// Returns hash of password
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        private string GetHash(Credential credential)
        {
            //Encrypts password and sets to hash credential
            byte[] password = Encoding.ASCII.GetBytes(credential.Password);
            byte[] hash = this.encryptionService.Encrypt(String.Empty, password);
            string hashString = Convert.ToBase64String(hash);
            return hashString;
        }
        
    }
}
