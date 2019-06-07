using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Consumer.Factories
{
    public interface ISmtpClientFactory
    {
        SmtpClient GetSmtpClient();
    }
}
