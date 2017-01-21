using Azure.Entities;
using Azure.Services.IServices;
using Infrastructure.IServices;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Services.Services
{
    /// <summary>
    /// Service in charge of managing of bus notifications
    /// </summary>
    public class BusService : IBusService
    {
        IIdentifierMachineService identifierMachineService;
        QueueClient client;

        public event EventHandler<NotificationBase> ReceivedData;

        public BusService(IIdentifierMachineService identifierMachineService)
        {
            this.identifierMachineService = identifierMachineService;
        }

        /// <summary>
        /// Initializes bus service
        /// </summary>
        public void Initialize()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["Endpoint"];
            string queueName = this.identifierMachineService.GetUniqueIdentifier();

            //Creates a bus if not exist
            this.CreateBusService(connectionString, queueName);

            client = QueueClient.CreateFromConnectionString(connectionString, queueName, ReceiveMode.ReceiveAndDelete);
            
            //Launches the event to receive a message
            client.OnMessage(messageReceived =>
            {
                NotificationBase deserializeMessage = this.GetDeserializeMessage(messageReceived);
                if (deserializeMessage != null)
                    this.ReceivedData(this, deserializeMessage);
            });
        }

        /// <summary>
        /// Close bus service
        /// </summary>
        public void CancelBus()
        {
            if (client != null)
                client.Close();//ToDo:Review this. I think doesn't work 
        }
        
        /// <summary>
        /// Creates a bus if not exist
        /// </summary>
        private void CreateBusService(string connectionString, string queueName)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            // Create queue
            if (!namespaceManager.QueueExists(queueName))
            {
                namespaceManager.CreateQueue(new QueueDescription(queueName)
                {
                    EnableDeadLetteringOnMessageExpiration=false,
                    DefaultMessageTimeToLive=TimeSpan.FromMinutes(1),
                    RequiresDuplicateDetection = false,
                    RequiresSession = false,
                    MaxSizeInMegabytes = 1024,
                });
            }
        }
        /// <summary>
        /// Deserialize brokered message in correct Type
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private NotificationBase GetDeserializeMessage(BrokeredMessage message)
        {
            NotificationBase deserializeMessage = null;
            if(message!=null && !String.IsNullOrEmpty(message.ContentType))
            {
                try
                {
                    Type messageType = Type.GetType(message.ContentType);
                    var method = typeof(BrokeredMessage).GetMethod("GetBody", new Type[] { });
                    var generic = method.MakeGenericMethod(messageType);
                    deserializeMessage = (NotificationBase)generic.Invoke(message, null);
                }
                catch (Exception) { }
            }
            return deserializeMessage;
        }
    }
}
