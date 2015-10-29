using ppatierno.AzureSBLite;
using ppatierno.AzureSBLite.Messaging;
using System;
using System.Text;

namespace Azure.FeedbackDevice.Common
{
    public class EventHub
    {
        public String ConnectionString { get; set; }

        public void Send(string eventHubEntity, MessageEntity message)
        {
            ServiceBusConnectionStringBuilder builder = new ServiceBusConnectionStringBuilder(this.ConnectionString);
            builder.TransportType = TransportType.Amqp;

            MessagingFactory factory = MessagingFactory.CreateFromConnectionString(this.ConnectionString);

            EventHubClient client = factory.CreateEventHubClient(eventHubEntity);

            EventData data = new EventData(Encoding.UTF8.GetBytes(message.ToJson()));

            client.Send(data);

            client.Close();
            factory.Close();
        }
    }
}
