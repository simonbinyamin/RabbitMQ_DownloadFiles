using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ_DownloadFiles
{
    public class Publisher
    {
        static void Main0()
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

                string message = "Yooo!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "my_stocks",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine("sent: {0}", message);
            }

            Console.WriteLine("exit.");
            Console.ReadLine();
        }
    }
}
