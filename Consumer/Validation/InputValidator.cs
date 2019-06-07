using Consumer.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Validation
{
    public class InputValidator : IInputValidator
    {
        public Message ProcessMessage(string messageContent)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(messageContent)) return new Message();

                var message = JsonConvert.DeserializeObject<Message>(messageContent);

                if (string.IsNullOrWhiteSpace(message.MailTo)) return new Message();

                return message;
            }
            catch (JsonSerializationException e)
            {
                throw new ArgumentNullException("Error deserializing JSON", e);
            }
        }
    }
}
