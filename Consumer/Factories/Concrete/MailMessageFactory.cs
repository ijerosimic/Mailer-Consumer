using Consumer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Consumer.Factories
{
    public class MailMessageFactory : IMailMessageFactory
    {
        public MailMessage CreateMailMessage(string sender, string subject, string body, ICollection<MessageAttachment> attachments)
        {
            if (string.IsNullOrWhiteSpace(sender) == false)
            {
                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(sender),
                    Subject = subject ?? string.Empty,
                    Body = body ?? string.Empty
                };

                foreach (var attachment in attachments)
                {
                    mailMessage.Attachments.Add(new Attachment(new MemoryStream(attachment.Body), attachment.Name, attachment.Type));
                }

                return mailMessage;
            }

            throw new ArgumentException("Sender email address cannot be null!");
        }
    }
}
