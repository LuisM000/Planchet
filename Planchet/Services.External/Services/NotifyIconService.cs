using Services.External.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services.External.Services
{
    /// <summary>
    /// Service in charge of create notify icon to close application
    /// </summary>
    public class NotifyIconService : INotifyIconService
    {
        ICloseUIService closeUI;
        Process sourceProcess;

        public NotifyIconService(ICloseUIService closeUI)
        {
            this.closeUI = closeUI;
        }

        /// <summary>
        /// Creates notify icon
        /// </summary>
        /// <param name="sourceProcess">Process that calls this method</param>
        public void CreateNotifyIcon(Process sourceProcess)
        {
            this.sourceProcess = sourceProcess;
            Thread thread = new Thread(() =>
            {
                this.CreateNotify(); 
            });

            thread.Start();
        }

        /// <summary>
        /// Generates and open notify icon
        /// </summary>
        private void CreateNotify()
        {
            NotifyIcon notificationIcon;
            ContextMenu menu = new ContextMenu();
            MenuItem menuExit = new MenuItem("Close");
            menu.MenuItems.Add(0, menuExit);
            notificationIcon = new System.Windows.Forms.NotifyIcon()
            {
                Icon = new Icon("Resources/Icon.ico"),
                ContextMenu = menu,
                Text = "Planchet"
            };
            menu.MenuItems[0].Click += ((sender, e) => this.closeUI.Open(sourceProcess));
            notificationIcon.DoubleClick += ((sender, e) => this.closeUI.Open(sourceProcess));
            notificationIcon.Visible = true;
            System.Windows.Forms.Application.Run();
        }
 
    }
}
