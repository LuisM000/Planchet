using Azure.Entities;
using Infrastructure.IFactories;
using Services.External.IServices;
using Services.External.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services.External.Services
{
    /// <summary>
    /// In charge of opens notification form
    /// </summary>
    public class NotificationUIService : INotificationUIService
    {
        ICommandFactory commandFactory;
        public NotificationUIService(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        /// <summary>
        /// Opens message notification form
        /// </summary>
        /// <param name="message"></param>
        public void OpenMessage(Azure.Entities.Message message)
        {
            new Thread(() => new MessageForm(this.commandFactory, message).ShowDialog()).Start();
        }
    }
}
