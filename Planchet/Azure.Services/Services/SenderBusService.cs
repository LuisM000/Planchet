using Azure.Entities;
using Azure.Services.IServices;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Services.Services
{
    /// <summary>
    /// In charge of sending notifications
    /// </summary>
    public class SenderBusService : ISenderBusService
    {
        /// <summary>
        /// Returns all paths of queues
        /// </summary>
        /// <returns></returns>
        public IList<string> GetQueues()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["Endpoint"];
            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            return namespaceManager.GetQueues().Select(q => q.Path).ToList();
        }

        /// <summary>
        /// Sends a bus notification
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public bool SendNotification(NotificationBase notification)
        {
            //Creates queueu
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["Endpoint"];
            string queueName = System.Configuration.ConfigurationManager.AppSettings["Queue"];
            var client = QueueClient.CreateFromConnectionString(connectionString, queueName);

            //Creates a brokered message based on notification.
            BrokeredMessage brokeredMessage = new BrokeredMessage(notification);
            brokeredMessage.ContentType = notification.GetType().AssemblyQualifiedName;
            //Sends message to queue
            client.Send(brokeredMessage);

            return true;
        }
    }
}
