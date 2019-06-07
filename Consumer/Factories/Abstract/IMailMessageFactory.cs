using Consumer.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Consumer.Factories
{
    public interface IMailMessageFactory
    {
        MailMessage CreateMailMessage(string sender, string subject, string body, ICollection<MessageAttachment> attachments);
    }
}
