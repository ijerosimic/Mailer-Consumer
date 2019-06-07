using Consumer.Messaging;
using Consumer.Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    public class ConnectionCreator : IConnectionCreator
    {
        static IConnection connection;
        static IModel channel;
        static ConnectionFactory factory;
        static readonly string hostName = "localhost";
        static readonly string userName = "igor";
        static readonly string password = "q1w2e3r4t5";

        public IMailCreator _mailCreator { get; }

        public ConnectionCreator(IMailCreator mailCreator) => _mailCreator = mailCreator;

        public void CreateConnection()
        {
            factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            try
            {
                connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                Thread.Sleep(10000);
                connection = factory.CreateConnection();
            }

            EstablishConnectionToQueue();
        }

        public void EstablishConnectionToQueue()
        {
            channel = connection.CreateModel();
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            try 
            {
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var messageContent = Encoding.UTF8.GetString(body);

                    channel.BasicAck(ea.DeliveryTag, false);

                    if (ea.DeliveryTag > 0 && (string.IsNullOrWhiteSpace(messageContent) == false && messageContent != "null"))
                    {
                        Console.WriteLine("received message: {0}", body);
                        //Thread.Sleep(1000);
                        await _mailCreator.CreateMail(messageContent);
                    }
                };

                channel.BasicConsume(queue: "q1",
                           autoAck: false,
                           consumer: consumer);
            }
            catch (Exception)
            {
                Thread.Sleep(10000);
                EstablishConnectionToQueue();
            }
        }
    }
}
