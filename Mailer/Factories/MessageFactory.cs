using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprossMailer.Factories
{
    public class MessageFactory : IMessageFactory
    {
        public Message CreateMessage(Message messageContent)
        {
            Message message;

            if (messageContent == null)
                return new Message();

            return message = new Message()
            {
                MailTo = messageContent.MailTo,
                Cc = messageContent.Cc,
                Bcc = messageContent.Bcc,
                Subject = messageContent.Subject,
                Body = messageContent.Body,
                IsSent = false,
                TriggerTime = DateTime.Now
            };
        }
    }
}
