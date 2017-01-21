using Model.Settings;
using Services.External.IServices;
using Services.External.WindowsForms;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.External.Services
{
    /// <summary>
    /// In charge of opens close windows form application
    /// </summary>
    public class CloseWindowsFormService : ICloseUIService
    {
        
        ICredentialsService credentialsService;

        public CloseWindowsFormService(ICredentialsService credentialsService)
        {
            this.credentialsService = credentialsService;
        }

        /// <summary>
        /// Opens close windows form and if credentials are correct, kills sourceProcess.
        /// </summary>
        /// <param name="sourceProcess"></param>
        public void Open(Process sourceProcess)
        {
            //ToDo: Check for any open
            CloseForm closeConsole = new CloseForm(sourceProcess,credentialsService);
            closeConsole.Show();
        }

 
    }
}
