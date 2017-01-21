using Model.Settings;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services.External.WindowsForms
{
    public partial class CloseForm : Form
    {
        ICredentialsService credentialsService;
        Process process;

        public CloseForm(Process process, ICredentialsService credentialsService)
        {
            this.process = process;
            this.credentialsService = credentialsService;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Credential credential = new Credential();
            credential.User = this.txtboxUser.Text;
            credential.Password = this.txtboxPassword.Text;
            //If the credentials are correct, close process
            if (this.credentialsService.IsCorrect(credential))
                process.Kill();
            else
                MessageBox.Show("Incorrect credentiales","Error");
        }
 
       
    }
}
