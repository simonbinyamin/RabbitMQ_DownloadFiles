using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ_DownloadFiles
{


    class Consumer
    {
        static void Main()
        {
            var connfactory = new ConnectionFactory() { HostName = "localhost" };

            using (var conn = connfactory.CreateConnection())
            using (var channel = conn.CreateModel())
            {
                channel.QueueDeclare(queue: "my_stocks",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, b) =>
                {
                    var body = b.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("received: {0}", message);
                };

                channel.BasicConsume(queue: "my_stocks",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("exit.");
                Console.ReadLine();
            }
        }

    }
}
