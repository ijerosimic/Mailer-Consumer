using AprossMailer.Models;
using AprossMailer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AprossMailer.Services
{
    public class ApiService : IApiService
    {
        IApiRepository _repo { get; }
        public ApiService(IApiRepository repo) => _repo = repo;

        public async Task PublishMessage(Message messageContent, List<MessageAttachment> filesList)
        {
            Message message;

            try
            {
                message = await SaveMessageToDatabase(messageContent, filesList);
            }
            catch (DbUpdateException e)
            {
                throw new DbUpdateException("Error saving message to database!", e);
            }

            EstablishConnectionToQueue(JsonConvert.SerializeObject(message, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }
        public async Task<Message> SaveMessageToDatabase(Message message, List<MessageAttachment> attachments)
        {
            return await _repo.SaveMessageToDatabase(message, attachments);
        }

        public void EstablishConnectionToQueue(string message)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = "igor", Password = "q1w2e3r4t5" };

                IConnection connection = factory.CreateConnection();
                IModel channel = connection.CreateModel();
                IBasicProperties basicProperties = channel.CreateBasicProperties();

                channel.ExchangeDeclare("RabbitMQTest", ExchangeType.Fanout, true);
                channel.QueueDeclare("q1", true, false, false);
                channel.QueueBind("q1", "RabbitMQTest", "", new Dictionary<string, object>());

                basicProperties.Persistent = true;
                basicProperties.ContentType = "text/plain";
                channel.BasicPublish("RabbitMQTest", "", basicProperties, Encoding.UTF8.GetBytes(message));
            }
            catch (Exception e)
            {
                throw new Exception("Error establishing connection to queuing server", e);
            }
        }
    }
}