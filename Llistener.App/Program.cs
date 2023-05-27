using Llistener.App.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

internal class Program 
{
    static async Task Main(string[] args)
    {
        //IMessageConsumer messageConsumer = new MessageConsumer();
        //messageConsumer.ConsumeMessage();
        //Console.ReadKey();

        ConnectionFactory factory = new ConnectionFactory();
        factory.HostName = ConsumerConstants.HostName;
        var connection = factory.CreateConnection();
        using var paymentApiChannel = connection.CreateModel();
        paymentApiChannel.QueueDeclare(ConsumerConstants.PaymentQueueName, exclusive: false);
        var consumer = new EventingBasicConsumer(paymentApiChannel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            PostPaymentMessagePayload(message);
            Console.WriteLine($"Message received from payment queue is : {message}");
        };
        paymentApiChannel.BasicConsume(queue: ConsumerConstants.PaymentQueueName, autoAck: true, consumer: consumer);

        var registrationConnection = factory.CreateConnection();
        using var registrationApiChannel = registrationConnection.CreateModel();
        registrationApiChannel.QueueDeclare(ConsumerConstants.RegisterQueueName, exclusive: false);
        var registrationConsumer = new EventingBasicConsumer(registrationApiChannel);
        registrationConsumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            PostRegistrationMessagePayload(message);
            Console.WriteLine($"Message received from registration queue is : {message}");
        };
        registrationApiChannel.BasicConsume(queue: ConsumerConstants.RegisterQueueName, autoAck: true, consumer: registrationConsumer);

        Console.ReadKey();
    }

    public static async void PostPaymentMessagePayload(string message)
    {
        using (var client = new HttpClient())
        {
            StringContent httpContent = new StringContent(message, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("https://localhost:7003/Email/sendPaymentEmail", httpContent);
        }
    }

    public static async void PostRegistrationMessagePayload(string message)
    {
        using (var client = new HttpClient())
        {
            StringContent httpContent = new StringContent(message, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("https://localhost:7003/Email/sendRegistrationEmail", httpContent);
        }
    }

}

