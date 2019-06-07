using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Service
{
    public interface IMailSender
    {
        Task SendMail(MailRepository mail);
    }
}
