using Llistener.App.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Llistener.App.Service
{
    public class MessageConsumer : IMessageConsumer
    {
        public void ConsumeMessage()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = ConsumerConstants.HostName;
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(ConsumerConstants.PaymentQueueName, exclusive: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Message received from queue is : {message}");
            };
            channel.BasicConsume(queue: ConsumerConstants.PaymentQueueName, autoAck: true, consumer: consumer);
        }
    }
}
